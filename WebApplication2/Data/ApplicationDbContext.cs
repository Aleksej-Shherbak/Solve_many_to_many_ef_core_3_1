using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        
        public ApplicationDbContext(IConfiguration configuration)
        {
            _connectionString = configuration
                .GetConnectionString("OurDatabase");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClassStudent>()
                .HasKey(t => new {t.StudentId, t.ClassId});
        }
    }
}