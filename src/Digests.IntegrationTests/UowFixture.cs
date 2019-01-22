using System;
using Digests.Data.EfCore.Uow;
using Shared.Kernel.Enums;

namespace Digests.IntegrationTests
{
    /// <summary>
    /// Инициализация и очиска тестовой бд через UOW.
    /// </summary>
    public class UowFixture : IDisposable
    {
        public EfUowDigests Uow { get; }

        public UowFixture()
        {
            var conStr = "Host=localhost;Port=5432;Database=DigestsDbTest;Username=postgres;Password=dmitr";
            Uow = EfUowDigests.EfUowDigestsFactory(conStr);
            Uow.CreateDb(HowCreateDb.Migrate).Wait();
        }

        public void Dispose()
        {
            Uow.WallMaterialRepository.Delete(wm => true);
            Uow.SaveChanges();
        }
    }
}