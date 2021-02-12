using ASO.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NETCore.MailKit;
using NETCore.MailKit.Core;
using NETCore.MailKit.Infrastructure.Internal;

namespace ASO.Services.Bootstrap
{
    public static class ServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddSingleton<IEmailService, EmailService>(_ => new EmailService(new MailKitProvider(MailOptions)));
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUsersService, UsersService>();  
            services.AddScoped<IRegisterService, RegisterService>();  
        }

        private static MailKitOptions MailOptions => new MailKitOptions
        {
            Account = "driving-school.online@yandex.ru",
            Password = "ctvrvmpluteoamlw",
            Server = "smtp.yandex.ru",
            Port = 465,
            Security = true,
            SenderEmail = "driving-school.online@yandex.ru",
            SenderName = "ASO Online"
        };
    }
}