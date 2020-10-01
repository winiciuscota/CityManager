using Microsoft.EntityFrameworkCore;
using CityManager.Domain.Entities;
using CityManager.Data.Configurations;
using System.Reflection;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace CityManager.Data
{
    public class CityManagerContext : DbContext
    {
        public DbSet<City> Cities { get; set; }

        public CityManagerContext(DbContextOptions<CityManagerContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfiguration(new CityConfiguration());
        }
    }
}