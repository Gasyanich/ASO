using System.Collections.Generic;
using ASO.DataAccess;
using ASO.DataAccess.Entities;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ASO.Services.Bootstrap
{
    public static class ConfigureIdentityServerExtension
    {
        private const int MonthSeconds = DaySeconds * 30;
        private const int DaySeconds = 60 * 60 * 24;

        private static readonly IEnumerable<IdentityResource> IdentityResources = new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };


        private static readonly IEnumerable<Client> Clients = new List<Client>
        {
            new Client
            {
                ClientId = "spa.aso.react",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowOfflineAccess = true,
                RequireClientSecret = false,
                AccessTokenLifetime = DaySeconds,
                AbsoluteRefreshTokenLifetime = MonthSeconds,
                AllowAccessTokensViaBrowser = true,
                AllowedScopes =
                {
                    "ASO.API"
                },
                AllowedCorsOrigins =
                {
                    "http://localhost:6001"
                }
            }
        };

        private static readonly IEnumerable<ApiScope> ApiScopes = new List<ApiScope>
        {
            new ApiScope("ASO.API", "Autoschool online api", new List<string> {"role"})
        };

        public static void ConfigureIdentityServer(this IServiceCollection services)
        {
            services.AddIdentityCore<User>()
                .AddRoles<IdentityRole<long>>()
                .AddUserManager<UserManager<User>>()
                .AddRoleManager<RoleManager<IdentityRole<long>>>()
                .AddSignInManager<SignInManager<User>>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;
            });

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityResources)
                .AddInMemoryClients(Clients)
                .AddInMemoryApiScopes(ApiScopes)
                .AddAspNetIdentity<User>();
        }
    }
}