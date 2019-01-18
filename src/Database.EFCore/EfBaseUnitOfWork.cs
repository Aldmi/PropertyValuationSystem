using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Kernel.Enums;

namespace Database.EFCore
{
    public class EfBaseUnitOfWork
    {
        private readonly DbContext _dbContext;

        public EfBaseUnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public virtual async Task CreateDb(HowCreateDb howCreateDb)
        {
            switch (howCreateDb)
            {
                case HowCreateDb.Migrate:
                    await _dbContext.Database.MigrateAsync();       //Если БД нет, то создать по схемам МИГРАЦИИ.
                    break;
                case HowCreateDb.EnsureCreated:
                    _dbContext.Database.EnsureCreated();           //Если БД нет, то создать. (ОТКЛЮЧАТЬ ПРИ МИГРАЦИИ). Нельзя применять миграции к БД созданной через EnsureCreated
                    break;
            }
        }
    }
}