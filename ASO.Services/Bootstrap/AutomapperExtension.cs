using System.Reflection;
using ASO.Models.Requests;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ASO.Services.Bootstrap
{
    public static class AutomapperExtension
    {
        public static void ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(RegisterRequest).Assembly);
        }
    }
}