using ASO.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ASO.Services.Bootstrap
{
    public static class ServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}