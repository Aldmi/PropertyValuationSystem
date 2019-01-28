using System;
using System.Linq;
using Digests.Data.EfCore.DbContext;

namespace Digests.Data.IntegrationTests.DbContextRow
{
    public class DbContextFixture : IDisposable
    {
        public Context Context { get; }

      

        public DbContextFixture()
        {
            var conStr = "Host=localhost;Port=5432;Database=DigestsDbRowTest;Username=postgres;Password=dmitr";
            Context= new Context(conStr);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
           // Context.Database.EnsureDeleted();
           var companyes=  Context.Companys.ToList();
           var wallMaterials = Context.WallMaterials.ToList();
           Context.Companys.RemoveRange(companyes);
           Context.WallMaterials.RemoveRange(wallMaterials);
           Context.SaveChanges();
        }
    }
}