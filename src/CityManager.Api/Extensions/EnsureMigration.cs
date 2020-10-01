using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using CityManager.Api.ViewModel;
using CityManager.Domain.Entities;
using CityManager.Domain.Repositories.Interfaces;
using CsvHelper;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CityManager.Api.Extensions
{
    public static class EnsureMigration
    {
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<T>();
                context.Database.Migrate();
                SeedDatabase(serviceScope);
            }
        }

        private static void SeedDatabase(IServiceScope serviceScope)
        {
            var cityRepository = serviceScope.ServiceProvider.GetService<ICityRepository>();
            var uoW = serviceScope.ServiceProvider.GetService<IUnitOfWork>();
            var env = serviceScope.ServiceProvider.GetService<IWebHostEnvironment>();
            
            string resourceName = "cities-seed.csv";

            var count = cityRepository.CountAsync().Result;
            if (count == 0)
            {
                var path = Path.Join(env.ContentRootPath, resourceName);
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
                    csv.Configuration.Delimiter = ";";
                    var cities = csv.GetRecords<EditCityViewModel>();

                    foreach (var city in cities)
                    {
                        cityRepository.Add(city.Adapt<City>());
                    }
                    uoW.Complete();
                }
            }
        }
    }
}