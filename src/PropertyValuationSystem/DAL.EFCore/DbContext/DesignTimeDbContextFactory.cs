using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Shared.ForConfigFiles;


namespace DAL.EFCore.DbContext
{
    /// <summary>
    /// Получение контекста для системы миграции (если конструктор контекста принимает парметры)
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var path = @"D:\Git\PropertyValuationSystem\src\PropertyValuationSystem\WebApi";
            var config = JsonConfigLib.GetConfiguration(path);
            var connectionString = config.GetConnectionString("MainDbConnection");
            return new Context(connectionString, false);
        }
    }
}