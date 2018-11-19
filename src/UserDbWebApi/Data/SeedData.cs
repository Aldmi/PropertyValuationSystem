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
                var companyScoring = new Company { Name = "Скоринг" };

                //ROLES---------------------------
                //Добавить РОЛЬ SuperAdmin
                string roleStr = "SuperAdmin";  
                var role = new IdentityRole(roleStr);
                var result= roleMgr.CreateAsync(role).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                var claims = new Dictionary<string, string>
                {
                    {"Acceess2_Tab1", "true"},
                    {"Acceess2_Tab2", "true"},
                    {"Acceess2_Tab3", "true"},
                    {"Acceess2_Tab4", "true"},
                    {"Acceess2_Tab5", "true"}
                }.Select(c=> new Claim(c.Key, c.Value)).ToList();
                foreach (var cliam in claims)
                {
                    result = roleMgr.AddClaimAsync(role, cliam).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }

                //Добавить РОЛЬ Admin
                roleStr = "Admin";
                role = new IdentityRole(roleStr);
                result= roleMgr.CreateAsync(role).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                foreach (var cliam in claims)
                {
                    result = roleMgr.AddClaimAsync(role, cliam).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }
     

                //USERS-----------------------------     
                var alice = userMgr.FindByNameAsync("alice").Result;
                if (alice == null)
                {
                    alice = new ApplicationUser
                    {
                        UserName = "alice",
                        Company = new Company { Name = "Супер Компания" }
                    };
                    result = userMgr.CreateAsync(alice, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                
                    roleStr = "SuperAdmin";
                    result = userMgr.AddToRoleAsync(alice, roleStr).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(alice, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim("CompanyName", alice.Company.Name),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }")
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }
                else
                {
                    Console.WriteLine("alice already exists");
                }


                var bob = userMgr.FindByNameAsync("bob").Result;
                if (bob == null)
                {
                    bob = new ApplicationUser
                    {
                        UserName = "bob",
                        Company = companyScoring
                    };
                    result = userMgr.CreateAsync(bob, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    roleStr = "Admin";
                    result = userMgr.AddToRoleAsync(bob, roleStr).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(bob, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim("CompanyName", bob.Company.Name),
                        new Claim("Access2AddingNewOrder", "true"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }"),
                        new Claim("location", "somewhere"),
                        //IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine("bob created");
                }
                else
                {
                    Console.WriteLine("bob already exists");
                }
            }
        }
    }
}