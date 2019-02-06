using Digests.Data.EfCore.DbContext.EntitiConfiguration;
using Digests.Data.EfCore.Entities._4Company;
using Digests.Data.EfCore.Entities._4House;
using Microsoft.EntityFrameworkCore;

namespace Digests.Data.EfCore.DbContext
{
    public sealed class Context : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly string _connStr;  // строка подключенния


        #region Reps

        public DbSet<EfCompany> Companys { get; set; }
        public DbSet<EfWallMaterial> WallMaterials { get; set; }
        public DbSet<EfHouse> Houses { get; set; }

        #endregion



        #region ctor

        public Context(string connStr)
        {
            _connStr = connStr;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }

        #endregion



        #region Config

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connStr);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfiguration(new EfHouseConfig());
           modelBuilder.ApplyConfiguration(new EfCompanyConfig());
           base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}