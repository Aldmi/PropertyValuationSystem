using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServerApi_AspNetIdentity
{
    public class Config
    {
        // scopes define the resources in your system 
        //описание пользователя для OpenIdConnect. нужно для формирования IdToken (инфа про пользователя)
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var customProfile = new IdentityResource(
                name: "custom.profile",
                displayName: "Custom profile",
                claimTypes: new[]
                {
                    "role",
                    "CompanyName",
                    "Acceess2_Tab1",
                    "Acceess2_Tab2",
                    "Acceess2_Tab3",
                    "Acceess2_Tab4",
                    "Acceess2_Tab5",
                    "Acceess2_Tab6"
                });

            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(), // "sub" claim
                new IdentityResources.Profile(),//стандартные claims
                customProfile
            };
        }


        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>

            {   //может выдавать токены для ресурса (api) c именем "api1"
                new ApiResource("MainApi", "Main Api")
                {
                    UserClaims = new List<string>
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        "CompanyName",
                        "Acceess2_Tab1",
                        "Acceess2_Tab2",
                        "Acceess2_Tab3",
                        "Acceess2_Tab4",
                        "Acceess2_Tab5",
                        "Acceess2_Tab6"
                    }
                },
                //может выдавать токены для ресурса (api) c именем "UserDbApi"
                new ApiResource("UserDbApi", "UserDb Api")
                {
                    UserClaims = new List<string>
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        "CompanyName"
                    }
                }
            };
        }

        
        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    AlwaysIncludeUserClaimsInIdToken = true,        
                    RequireConsent = false,  //страница согласия

                    // белый список адресов на который клиентское приложение может попросить
                    // перенаправить User Agent, важно для безопасности
                    RedirectUris =
                    {
                        // адрес перенаправления после логина
                        "http://localhost:5003/callback.html",
                        // адрес перенаправления при автоматическом обновлении access_token через iframe
                        "http://localhost:5003/callback-silent.html"
                    },

                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },

                    // адрес клиентского приложения, просим сервер возвращать нужные CORS-заголовки
                    AllowedCorsOrigins =     { "http://localhost:5003" },

                    // список scopes (ресурсов), разрешённых для данного клиентского приложения
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "custom.profile",
                        "MainApi",
                        "UserDbApi"
                    },

                    AccessTokenLifetime = 300, // секунд, это значение по умолчанию
                    IdentityTokenLifetime = 3600, // секунд, это значение по умолчанию

                    // разрешено ли получение refresh-токенов через указание scope offline_access
                    AllowOfflineAccess = false,
                }
            };
        }
    }
}