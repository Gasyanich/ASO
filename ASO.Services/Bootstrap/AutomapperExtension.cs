using ASO.Models.DTO.Users;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ASO.Services.Bootstrap
{
    public static class AutomapperExtension
    {
        public static void ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserRegisterDto).Assembly);
        }
    }
}