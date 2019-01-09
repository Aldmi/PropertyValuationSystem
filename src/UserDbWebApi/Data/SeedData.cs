using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserDbWebApi.Entities;

namespace UserDbWebApi.Data
{
    public class SeedData
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                //ROLES-------------------------------------------------------------
                //Добавить РОЛЬ SuperAdmin

                string roleStr = "SuperAdmin";
                var role = roleMgr.FindByNameAsync(roleStr).Result;
                if (role == null)
                {
                    role = new IdentityRole(roleStr);
                    var result = roleMgr.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    var claims = new Dictionary<string, string>
                    {
                        {"Role_Acceess2_Tab1", "true"},
                        {"Role_Acceess2_Tab2", "true"},
                        {"Role_Acceess2_Tab3", "true"},
                        {"Role_Acceess2_Tab4", "true"},
                        {"Role_Acceess2_Tab5", "true"}
                    }.Select(c => new Claim(c.Key, c.Value)).ToList();
                    foreach (var cliam in claims)
                    {
                        result = roleMgr.AddClaimAsync(role, cliam).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                    }
                }

                roleStr = "Manager";
                role = roleMgr.FindByNameAsync(roleStr).Result;
                if (role == null)
                {
                    role = new IdentityRole(roleStr);
                    var result = roleMgr.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    var claims = new Dictionary<string, string>
                    {
                        {"Role_Acceess2_Tab1", "true"},
                        {"Role_Acceess2_Tab2", "true"},
                        {"Role_Acceess2_Tab3", "true"},
                        {"Role_Acceess2_Tab4", "false"},
                        {"Role_Acceess2_Tab5", "false"}
                    }.Select(c => new Claim(c.Key, c.Value)).ToList();
                    foreach (var cliam in claims)
                    {
                        result = roleMgr.AddClaimAsync(role, cliam).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                    }
                }

                roleStr = "Photograph";
                role = roleMgr.FindByNameAsync(roleStr).Result;
                if (role == null)
                {
                    role = new IdentityRole(roleStr);
                    var result = roleMgr.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    var claims = new Dictionary<string, string>
                    {
                        {"Acceess2_Tab1", "true"},
                        {"Acceess2_Tab2", "false"},
                        {"Acceess2_Tab3", "false"},
                        {"Acceess2_Tab4", "false"},
                        {"Acceess2_Tab5", "false"}
                    }.Select(c => new Claim(c.Key, c.Value)).ToList();
                    foreach (var cliam in claims)
                    {
                        result = roleMgr.AddClaimAsync(role, cliam).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                    }
                }

                //USERS------------------------------------------------------------------     
                //Добавить ПОЛЬЗОВАТЕЛЯ SuperAdmin
                roleStr = "SuperAdmin";
                var superAdmin = userMgr.FindByNameAsync("SuperAdmin").Result;
                if (superAdmin == null)
                {
                    superAdmin = new ApplicationUser
                    {
                        UserName = "SuperAdmin",
                        Company = new Company { Name = "Супер Компания" }
                    };
                    var result = userMgr.CreateAsync(superAdmin, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddToRoleAsync(superAdmin, roleStr).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(superAdmin, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim("CompanyName", superAdmin.Company.Name),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim("Acceess2_Tab1", "true"),
                        new Claim("Acceess2_Tab2", "true"),
                        new Claim("Acceess2_Tab3", "true"),
                        new Claim("Acceess2_Tab4", "true"),
                        new Claim("Acceess2_Tab5", "true")
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }
                else
                {
                    Console.WriteLine("SuperAdmin already exists");
                }
            }
        }
    }
}