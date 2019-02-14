using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DddTestApi.AutofacModules;
using Digests.Core.Model._4House;
using Digests.Data.Abstract;
using Digests.Data.EfCore.Mapper;
using Digests.Data.EfCore.Repositories;
using Digests.Data.EfCore.Uow;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Kernel.Enums;

namespace DddTestApi
{
    public class Startup
    {
        public Startup(IConfiguration appConfiguration)
        {
            AppConfiguration = appConfiguration;
        }

        public IConfiguration AppConfiguration { get; }




        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }


        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              ILifetimeScope scope)
        {
            InitializeAsync(scope).Wait();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }


        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connectionString = AppConfiguration.GetConnectionString("DigestsSubDomainDbConnectionUseNpgsql");
            builder.RegisterModule(new UnitOfWorkAutofacModule(connectionString));

            builder.RegisterModule(new ApplicationServicesAutofacModule());
        }


        /// <summary>
        /// Инициализация системы.
        /// </summary>
        private async Task InitializeAsync(ILifetimeScope scope)
        {
            //var logger = scope.Resolve<ILogger>();
            //СОЗДАНИЕ БД (если не созданно)--------------------------------------------------
            try
            {
                //TODO: с оздавать БД через UOW 
                var uow= scope.Resolve<IUnitOfWorkDigests>();
                await uow.CreateDb(HowCreateDb.EnsureCreated); // EnsureCreated 4 Debug


                //DEBUG-------
                //var uowTest = scope.Resolve<IUnitOfWorkDigests>();
                ////var newHouse = new House("Новосибирск", "Заельцовский", "Овражная", "11", 2013, "Заельцовская","56821.1 98562.2", new WallMaterial("Кирпич"));
                //var newWallMaterial= new WallMaterial("Кирпич");
                //await uowTest.WallMaterialRepository.AddAsync(newWallMaterial);          
                //await uowTest.SaveChangesAsync();

                ////uowTest = scope.Resolve<IUnitOfWorkDigests>();
                //await uowTest.WallMaterialRepository.DeleteAsync(wm => true);
                //await uowTest.SaveChangesAsync();
                //DEBUG-------
            }
            catch (Exception ex)
            {
                //logger.Fatal($"Ошибка создания БД на основе миграций {ex}");
            }
        }
    }
}
