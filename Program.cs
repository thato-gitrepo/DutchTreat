using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            //Instantiate Seeding
            SeedDb(host);
            
            host.Run();
        }

        private static void SeedDb(IWebHost host)
        {
            //create scope
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();

            //Close scope after scope run
            using(var scope = scopeFactory.CreateScope())
            {
                //Retrieve seeder object
                var seeder = host.Services.GetService<DutchSeeder>();
                seeder.Seed();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            //Make a call to...
            .ConfigureAppConfiguration(SetupConfiguration)
            .UseStartup<Startup>()
            .Build();

        private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
        {
            //Remove the default configuration options
            builder.Sources.Clear();

            //Path - 
            builder.AddJsonFile("config.json", false, true)
                .AddEnvironmentVariables();
        }
    }
}
