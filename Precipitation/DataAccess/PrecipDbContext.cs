using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Precipitation.DataAccess
{
    public class PrecipDbContext : DbContext 
    {
        public PrecipDbContext(DbContextOptions<PrecipDbContext> options): base(options)
        {
            
        }
        public DbSet<Precipitation> Precipitations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SnakeCaseIdentityTableNames(modelBuilder);
        }

        private void SnakeCaseIdentityTableNames(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Precipitation>().ToTable("precipitation");
        }
    }
}