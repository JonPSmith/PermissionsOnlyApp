using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PermissionsOnlyApp.Data;
using ServiceLayer.SeedDemo;

namespace PermissionsOnlyApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            (await BuildWebHostAsync(args)).Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static async Task<IHost> BuildWebHostAsync(string[] args)
        {
            var iHost = CreateHostBuilder(args)
                .Build();

            //This is my extra code I have added to the startup to
            //a) migrate/create the databases 
            //b) add a SuperUser so you can log in and see what happens
            MigrateDatabases(iHost);
            await CheckAddSuperAdminAsync(iHost);
            return iHost;
        }


        private static void MigrateDatabases(IHost iHost)
        {
            using var scope = iHost.Services.CreateScope();
            var services = scope.ServiceProvider;
            //it only creates the databases and seeds them if the DemoSetup:CreateAndSeed property is true

            using (var context = services.GetRequiredService<ApplicationDbContext>())
            {
                context.Database.Migrate();
            }

            //This creates a database which is a combination of both the ExtraAuthorizeDbContext and CompanyDbContext
            using (var context = services.GetRequiredService<MyDbContext>())
            {
                context.Database.Migrate();
            }
        }

        private static async Task CheckAddSuperAdminAsync(IHost iHost)
        {
            using var scope = iHost.Services.CreateScope();
            var services = scope.ServiceProvider;
            await services.CheckAddSuperAdminAsync();
        }

    }
}
