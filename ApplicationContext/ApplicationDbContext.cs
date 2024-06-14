using Microsoft.EntityFrameworkCore;
using TechNeuronstask.Models;

namespace TechNeuronstask.ApplicationContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Employee entity
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id); // Set Id as primary key

                // Configure Id to be manually entered (not auto-incrementing)
                entity.Property(e => e.Id)
                      .ValueGeneratedNever();

                // Other configurations if needed
            });
        }
    }
}
