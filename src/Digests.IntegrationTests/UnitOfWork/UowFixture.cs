using System;
using Digests.Data.EfCore.Uow;
using Shared.Kernel.Enums;

namespace Digests.Data.IntegrationTests.UnitOfWork
{
    /// <summary>
    /// Инициализация и очиска тестовой бд через UOW.
    /// </summary>
    public class UowFixture : IDisposable
    {
        public EfUowDigests Uow { get; }

        public UowFixture()
        {
            const string conStr = "Host=localhost;Port=5432;Database=DigestsDbUowTest;Username=postgres;Password=dmitr";
            Uow = EfUowDigests.UowDigestsFactory(conStr);
            Uow.CreateDb(HowCreateDb.EnsureCreated).Wait();
        }

        public void Dispose()
        {
            Uow.CompanyRepository.Delete(wm => true);
            Uow.WallMaterialRepository.Delete(wm => true);
            Uow.SaveChanges();
        }
    }
}