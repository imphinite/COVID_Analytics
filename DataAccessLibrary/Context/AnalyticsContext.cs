using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using DataAccessLibrary.Dimensions;

namespace DataAccessLibrary.Context
{
    public class AnalyticsContext : DbContext
    {
        public AnalyticsContext(DbContextOptions options) : base(options) { }

        /**
         * OLTP
         */
        public DbSet<AgeGroup> AgeGroups { get; set; }

        public DbSet<Case> Cases { get; set; }

        public DbSet<HealthAuthority> HealthAuthorities { get; set; }

        public DbSet<HealthServiceDeliveryArea> HealthServiceDeliveryAreas { get; set; }

        public DbSet<LabTestReport> LabTestReports { get; set; }

        public DbSet<Region> Regions { get; set; }

        /**
         * OLAP
         */
        public DbSet<DimAgeGroup> DimAgeGroups { get; set; }

        public DbSet<DimCase> DimCases { get; set; }

        public DbSet<DimHealthAuthority> DimHealthAuthorities { get; set; }

        public DbSet<DimHealthServiceDeliveryArea> DimHealthServiceDeliveryAreas { get; set; }

        public DbSet<DimLabTestReport> DimLabTestReports { get; set; }

        public DbSet<DimRegion> DimRegions { get; set; }
    }
}
