using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Context
{
    public class AnalyticsContext : DbContext
    {
        public AnalyticsContext(DbContextOptions options) : base(options) { }
    }
}
