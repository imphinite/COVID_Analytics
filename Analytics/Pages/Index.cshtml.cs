using DataAccessLibrary.Context;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using DataAccessLibrary.Models;
using Newtonsoft.Json.Linq;
using MoreLinq;
using System;

namespace Analytics.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AnalyticsContext _db;

        public IndexModel(ILogger<IndexModel> logger, AnalyticsContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet()
        {
            InitData();
        }

        private void InitData ()
        {
            //PurgeDB();

            InitRegions();
            InitHealthAuthorities();
            InitAgeGroup();

            LoadSampleData();
        }

        private void PurgeDB ()
        {
            _db.DimRegions.RemoveRange(_db.DimRegions);
            _db.HealthServiceDeliveryAreas.RemoveRange(_db.HealthServiceDeliveryAreas);
            _db.HealthAuthorities.RemoveRange(_db.HealthAuthorities);
            _db.Regions.RemoveRange(_db.Regions);

            _db.AgeGroups.RemoveRange(_db.AgeGroups);
            _db.Cases.RemoveRange(_db.Cases);

            _db.SaveChanges();
        }

        private void InitRegions ()
        {
            if (_db.Regions.Count() > 0) return;

            string file = System.IO.File.ReadAllText("RawData/regional_summary_210320.json");
            dynamic json = JsonConvert.DeserializeObject(file);

            List<Region> regions = new List<Region>();
            foreach (JObject rawRegion in json)
            {
                var region = new Region();
                region.Name = rawRegion["HA"].ToString();
                region.Province = rawRegion["Province"].ToString();

                regions.Add(region);
            }

            List<Region> distinct = regions.DistinctBy(r => new { r.Name, r.Province }).ToList();

            _db.AddRange(distinct);
            _db.SaveChanges();
        }

        private void InitHealthAuthorities ()
        {
            if (_db.HealthAuthorities.Count() > 0) return;

            string file = System.IO.File.ReadAllText("RawData/regional_summary_210320.json");
            dynamic json = JsonConvert.DeserializeObject(file);

            List<Tuple<string, string>> HA_HSDA = new List<Tuple<string, string>>();
            foreach (JObject record in json)
            {
                string HA = record["HA"].ToString();
                string HSDA = record["HSDA"].ToString();

                HA_HSDA.Add(Tuple.Create(HA, HSDA));
            }
            HA_HSDA = HA_HSDA.Distinct().ToList();

            List<HealthAuthority> healthAuthorities = new List<HealthAuthority>();
            foreach (Tuple<string, string> tuple in HA_HSDA)
            {
                HealthAuthority HA = healthAuthorities.Find(ha => ha.Region.Name == tuple.Item1);
                if (HA == null)
                {
                    var region = _db.Regions.Single(r => r.Name == tuple.Item1);
                    HA = new HealthAuthority();
                    HA.Region = region;

                    healthAuthorities.Add(HA);
                }

                HealthServiceDeliveryArea HSDA = new HealthServiceDeliveryArea();
                HSDA.Area = tuple.Item2;
                HA.HealthServiceDeliveryAreas.Add(HSDA);
            }

            _db.AddRange(healthAuthorities);
            _db.SaveChanges();
        }

        private void InitAgeGroup ()
        {
            if (_db.AgeGroups.Count() > 0) return;

            string file = System.IO.File.ReadAllText("RawData/case_details_210320.json");
            dynamic json = JsonConvert.DeserializeObject(file);

            List<AgeGroup> ageGroups = new List<AgeGroup>();
            foreach (JObject record in json)
            {
                AgeGroup ageGroup = new AgeGroup();
                ageGroup.Range = record["Age_Group"].ToString();

                ageGroups.Add(ageGroup);
            }

            List<AgeGroup> distinct = ageGroups.DistinctBy(ag => ag.Range).ToList();

            _db.AddRange(distinct);
            _db.SaveChanges();
        }

        private void LoadSampleData()
        {
            if (_db.Cases.Count() > 0) return;

            string file = System.IO.File.ReadAllText("RawData/case_details_210320.json");
            dynamic json = JsonConvert.DeserializeObject(file);


            List<Case> cases = new List<Case>();
            foreach (JObject record in json)
            {
                string reportedDate = record["Reported_Date"].ToString();
                string HA = record["HA"].ToString();
                string sex = record["Sex"].ToString();
                string ageGroup = record["Age_Group"].ToString();
                string classificationReported = record["Classification_Reported"].ToString();

                try
                {
                    // Find AgeGroup
                    AgeGroup ag = _db.AgeGroups.Single(a => a.Range == ageGroup);
                    // Find HealthAuthority
                    HealthAuthority ha = _db.HealthAuthorities.Where(h => h.Region.Name == HA).Single();

                    Case COVID_case = new Case();
                    COVID_case.ReportedDate = DateTime.Parse(reportedDate);
                    COVID_case.HealthAuthority = ha;
                    COVID_case.Sex = sex;
                    COVID_case.AgeGroup = ag;
                    COVID_case.ClassificationReported = classificationReported;

                    cases.Add(COVID_case);
                }
                catch (Exception e)
                {
                    continue;
                }
            }

            _db.AddRange(cases);
            _db.SaveChanges();
        }
    }
}
