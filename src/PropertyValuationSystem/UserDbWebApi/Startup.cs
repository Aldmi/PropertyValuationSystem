using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UserDbWebApi.Data;
using UserDbWebApi.Entities;
using UserDbWebApi.Services;

namespace UserDbWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }



        public void ConfigureServices(IServiceCollection services)
        {
            //Контекст имеет scope НЕ PerRequest а берется из пула (переиспользуется)
            services.AddDbContextPool<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>() //TODO: Не работают сервисы для Users, Roles
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<RoleManagerService>();
            services.AddTransient<UserManagerService>();

            services.AddMvcCore()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                // добавляем авторизацию, благодаря этому будут работать атрибуты Authorize
                .AddAuthorization(options =>
                {
                    options.AddPolicy("AdminsOnly", policyUser =>
                    {
                        policyUser.RequireClaim("role", "admin");
                        //policyUser.RequireClaim("Access2Read", "true");
                    });
                    options.AddPolicy("ManagerOnly", policyUser =>
                    {
                        policyUser.RequireClaim("role", "manager");
                        //policyUser.RequireClaim("Access2Read", "true");
                    });
                })
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";   //адресс сервера авторизации
                    options.RequireHttpsMetadata = false;          // Https - нет
                    options.ApiName = "UserDbApi";                 // scope(resource) с именем "UserDbApi"
                    options.EnableCaching = true;
                    options.CacheDuration = TimeSpan.FromMinutes(10);
                });

            services.AddCors(options =>
            {
                // задаём политику CORS, чтобы наше клиентское приложение могло отправить запрос на этот Адресс
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:5003")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }




        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("default");
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
