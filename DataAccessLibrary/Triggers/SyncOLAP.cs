using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Triggers
{
    public abstract class SyncOLAP
    {
        public virtual DbContext Db { get; set; }
        public virtual object Dimension { get; set; }

        static SyncOLAP()
        {
            Triggers<SyncOLAP>.Inserting += entry =>
            {
                Console.WriteLine("Inserting " + entry.Entity);

                entry.Entity.Db.Add(entry.Entity.Dimension);
                entry.Entity.Db.SaveChanges();
            };
        }
    }
}
