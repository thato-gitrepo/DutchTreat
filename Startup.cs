using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AutoMapper;
using System.Reflection;

namespace DutchTreat
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Add DB Context to service Collection
            services.AddDbContext<DutchContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("DutchConnectionString"));
            });

            //Register seeder - AddTransient : Can inject as a Service
            services.AddTransient<DutchSeeder>();

            //Inject Automapper into Controllers - Look for profiles
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Support for mail service
            services.AddTransient<INullMailService, NullMailService>();

            //Register the repository to the Service layer
            services.AddScoped<IDutchRepository, DutchRepository>();

            //Call MVC Views
            //services.AddControllersWithViews();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //Serves HTML directly - black page (change internal path to index page)
            //app.UseDefaultFiles();

            //Display exception page - for developers
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //Error page
                app.UseExceptionHandler("/error");
            }

            //static files saved in wwwroot dir - treated as the root of the web server
            app.UseStaticFiles();

            //Use node modules
            app.UseNodeModules();

            //Enable routing
            app.UseRouting();

            //Enable multiple subsystems - listen to individual endpoints
            app.UseEndpoints(cfg =>
            {
                cfg.MapControllerRoute("Fallback",
                    "{controller}/{action}/{id?}",
                    new { controller = "App", action = "Index" });
            });
        }
    }
}
