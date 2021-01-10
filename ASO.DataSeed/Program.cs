using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASO.DataAccess.Entities;
using ASO.Models.Constants;
using ASO.Services.Bootstrap;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASO.DataSeed
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();

            serviceCollection.AddDataProtection();
            serviceCollection.ConfigureDatabase(configuration);
            serviceCollection.ConfigureAspNetIdentity();
            serviceCollection.ConfigureAuth(configuration);
            serviceCollection.ConfigureAutomapper();
            serviceCollection.ConfigureServices();

            var services = serviceCollection.BuildServiceProvider();


            var roleManager = services.GetService<RoleManager<UserRole>>();
            foreach (var userRole in Roles)
            {
                await roleManager.CreateAsync(userRole);
            }

            var userManager = services.GetService<UserManager<User>>();

            await userManager.CreateAsync(Director);
            await userManager.AddToRoleAsync(Director, RolesConstants.Director);
        }

        private static List<UserRole> Roles = new()
        {
            new UserRole
            {
                DisplayName = "Директор",
                Name = RolesConstants.Director,
            },
            new UserRole
            {
                DisplayName = "Администратор",
                Name = RolesConstants.Admin,
            },
            new UserRole
            {
                DisplayName = "Преподаватель",
                Name = RolesConstants.Teacher,
            },
            new UserRole
            {
                DisplayName = "Менеджер",
                Name = RolesConstants.Manager,
            },
            new UserRole
            {
                DisplayName = "Обучающийся",
                Name = RolesConstants.Student,
            },
        };

        private static readonly User Director = new()
        {
            UserName = "svv-66@yandex.ru",
            Email = "svv-66@yandex.ru",
            FirstName = "Игорь",
            LastName = "Игуменов",
            Patronymic = "Александрович",
            PhoneNumber = "+79023217238"
        };
    }
}