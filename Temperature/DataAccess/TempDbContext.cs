using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Temperature.DataAccess
{
    public class TempDbContext : DbContext
    {
        public TempDbContext(DbContextOptions<TempDbContext> options): base(options)
        {
            
        }

        public DbSet<Temperature> Temperatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SnakeCaseIdentityTableNames(modelBuilder);
        }

        private void SnakeCaseIdentityTableNames(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Temperature>().ToTable("temperature");
        }
    }
}