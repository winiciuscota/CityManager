using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CityManager.Api.Extensions;
using CityManager.Data;
using CityManager.Data.Repositories;
using CityManager.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace CityManager.Api
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
            services.AddControllers();

            services.AddDbContext<CityManagerContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Default")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "City Manager",
                    Version = "1.0"
                });

                var currentProjectFilePath = Path.Combine(System.AppContext.BaseDirectory, "docs.xml");
                var domainProjectFilePath = Path.Combine(System.AppContext.BaseDirectory, "Domain/docs.xml");

                c.IncludeXmlComments(currentProjectFilePath);
                c.IncludeXmlComments(domainProjectFilePath);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICityRepository, CityRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "City Manager api");
                })
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                })
                .EnsureMigrationOfContext<CityManagerContext>();
        }
    }
}
