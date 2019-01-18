using System;
using Digests.Data.EfCore.Entities;
using Digests.Data.EfCore.Entities.HouseDigests;
using Microsoft.EntityFrameworkCore;

namespace Digests.Data.EfCore.DbContext
{
    public sealed class Context : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly string _connStr;  // строка подключенния


        #region Reps

        public DbSet<EfHouse> Houses { get; set; }
        public DbSet<EfWallMaterial> WallMaterials { get; set; }
        public DbSet<EfCompany> Companys { get; set; }

        #endregion



        #region ctor

        public Context(string connStr)
        {
            _connStr = connStr;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;//Отключение Tracking для всего контекста
        }

        #endregion



        #region Config

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Context сам получает строку подключения при миграции и работе.
            //(Рабоатет для миграции и работы!!!!!!!)
            // var config = JsonConfigLib.GetConfiguration();
            //var connectionString = config.GetConnectionString("MainDbConnection");
            //optionsBuilder.UseSqlServer(connectionString);
            //optionsBuilder.UseSqlServer(connectionString, ob => ob.MigrationsAssembly(typeof(Context).GetTypeInfo().Assembly.GetName().Name));\

            optionsBuilder.UseNpgsql(_connStr);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           //modelBuilder.ApplyConfiguration(new EfDeviceOptionConfig());
           base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}