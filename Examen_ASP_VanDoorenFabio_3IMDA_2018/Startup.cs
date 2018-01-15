using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Examen_ASP_VanDoorenFabio_3IMDA_2018.Entities;
using Examen_ASP_VanDoorenFabio_3IMDA_2018.Data;
using Examen_ASP_VanDoorenFabio_3IMDA_2018.Services;
using Microsoft.EntityFrameworkCore;
using Examen_ASP_VanDoorenFabio_3IMDA_2018.Controllers;

namespace Examen_ASP_VanDoorenFabio_3IMDA_2018
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EntityContext>(options => options.UseSqlite("Filename=./cars.db"));
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, EntityContext entityContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            DatabaseInitializer.InitializeDatabase(entityContext);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
