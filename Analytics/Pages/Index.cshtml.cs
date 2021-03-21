using DataAccessLibrary.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

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
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            //if (_db.Customers.Count() == 0) return;
            string file = System.IO.File.ReadAllText("Scripts/13100775_1.json");

            dynamic jsonObj = JsonConvert.DeserializeObject(file);


            //var customers = JsonSerializer.Deserialize<List<Customer>>(file);
            //_db.AddRange(customers);
            //_db.SaveChanges();
        }
    }
}
