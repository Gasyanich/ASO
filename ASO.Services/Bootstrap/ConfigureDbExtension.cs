using ASO.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASO.Services.Bootstrap
{
    public static class ConfigureDbExtension
    {
        public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(
                options => options.UseNpgsql(
                    configuration.GetConnectionString("AsoDb"),
                    builder => builder.MigrationsAssembly(typeof(DataContext).Assembly.FullName))
            );
        }
    }
}