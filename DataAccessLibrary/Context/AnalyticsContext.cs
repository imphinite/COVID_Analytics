using Microsoft.EntityFrameworkCore;
using DataAccessLibrary.Models;
using DataAccessLibrary.Dimensions;
using System;
using EntityFrameworkCore.Triggers;
using System.Threading.Tasks;
using System.Threading;

namespace DataAccessLibrary.Context
{
    public class AnalyticsContext : DbContext
    {
        public AnalyticsContext(DbContextOptions options) : base(options) { }

        ///**
        // * Enable triggers
        // */
        //public override Int32 SaveChanges()
        //{
        //    return this.SaveChangesWithTriggers(base.SaveChanges, acceptAllChangesOnSuccess: true);
        //}
        //public override Int32 SaveChanges(Boolean acceptAllChangesOnSuccess)
        //{
        //    return this.SaveChangesWithTriggers(base.SaveChanges, acceptAllChangesOnSuccess);
        //}
        //public override Task<Int32> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, acceptAllChangesOnSuccess: true, cancellationToken: cancellationToken);
        //}
        //public override Task<Int32> SaveChangesAsync(Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, acceptAllChangesOnSuccess, cancellationToken);
        //}

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
