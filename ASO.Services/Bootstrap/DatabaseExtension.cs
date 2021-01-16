using ASO.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASO.Services.Bootstrap
{
    public static class DatabaseExtension
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(
                options => options.UseNpgsql(
                    configuration.GetConnectionString("AsoDb"),
                    builder => builder.MigrationsAssembly(typeof(DataContext).Assembly.FullName))
            );
        }
    }
}