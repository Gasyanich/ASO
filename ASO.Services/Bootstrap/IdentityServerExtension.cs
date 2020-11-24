﻿using System.Collections.Generic;
using ASO.DataAccess.Entities;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ASO.Services.Bootstrap
{
    public static class IdentityServerExtension
    {
        public static void ConfigureIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityServer()
                .AddInMemoryIdentityResources(IdentityResources)
                .AddInMemoryClients(Clients)
                .AddInMemoryApiScopes(ApiScopes)
                .AddAspNetIdentity<User>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["IdentityServerAddress"];

                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateActor = false,
                        ValidateIssuerSigningKey = false
                    };
                });
        }

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
                }
            }
        };

        private static readonly IEnumerable<ApiScope> ApiScopes = new List<ApiScope>
        {
            new ApiScope("ASO.API", "Autoschool online api", new List<string> {"role"})
        };
    }
}