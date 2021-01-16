using ASO.DataAccess;
using ASO.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ASO.Services.Bootstrap
{
    public static class AspNetIdentityExtension
    {
        public static void ConfigureAspNetIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<User>()
                .AddRoles<UserRole>()
                .AddUserManager<UserManager<User>>()
                .AddRoleManager<RoleManager<UserRole>>()
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
        }
    }
}