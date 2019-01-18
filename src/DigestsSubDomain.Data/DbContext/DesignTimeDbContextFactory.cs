using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Shared.ForConfigFiles;

namespace DigestsSubDomain.Data.DbContext
{
    /// <summary>
    /// Получение контекста для системы миграции (если конструктор контекста принимает парметры)
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var path = @"D:\\Git\\PropertyValuationSystem\\src\\WebApi";
            //var hh=  Directory.Exists(path);
            //Console.WriteLine(hh.ToString());
            var config = JsonConfigLib.GetConfiguration(path);
            var connectionString = config.GetConnectionString("MainDbConnection");
            return new Context(connectionString, false);
        }
    }
}