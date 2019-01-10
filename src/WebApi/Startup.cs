using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using Shared.ForConfigFiles;
using WebApi.AutofacModules;
using WebApi.Extensions;

namespace WebApi
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
            services.AddSerilogServices();

            services.AddMvcCore()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddControllersAsServices()
                    .AddJsonOptions(o =>
                    {
                        o.SerializerSettings.Formatting = Formatting.Indented;
                        o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    })
                    // добавляем авторизацию, благодаря этому будут работать атрибуты Authorize (Роли и Политики)
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
                         options.AddPolicy("Acceess2_Tab1_Policy", policyUser =>
                         {
                             policyUser.RequireClaim("Acceess2_Tab1", "true");
                         });
                     })
                    .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";   //адресс сервера авторизации
                    options.RequireHttpsMetadata = false;          // Https - нет
                    options.ApiName = "MainApi";                   // scope(resource) с именем "apiMain"
                    //options.EnableCaching = true;
                    //options.CacheDuration = TimeSpan.FromMinutes(10);
                    options.JwtBearerEvents = new JwtBearerEvents
                    {
                        //OnMessageReceived = e =>
                        //{
                        //    _logger.LogTrace("JWT: message received");
                        //    return Task.CompletedTask;
                        //},

                        //OnTokenValidated = e =>
                        //{
                        //    _logger.LogTrace("JWT: token validated");
                        //    return Task.CompletedTask;
                        //},

                        //OnAuthenticationFailed = e =>
                        //{
                        //    _logger.LogTrace("JWT: authentication failed");
                        //    return Task.CompletedTask;
                        //},

                        //OnChallenge = e =>
                        //{
                        //    _logger.LogTrace("JWT: challenge");
                        //    return Task.CompletedTask;
                        //}
                    };
                });

            services.AddCors(options =>
            {
                // задаём политику CORS, чтобы наше клиентское приложение могло отправить запрос на сервер API
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:5003")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }


        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connectionString = AppConfiguration.GetConnectionString("MainDbConnection");
            builder.RegisterModule(new RepositoryAutofacModule(connectionString));
            builder.RegisterModule(new MediatorsAutofacModule());
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
