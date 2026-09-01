using Microsoft.EntityFrameworkCore;
using Data.Models;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Contractor> Contractors { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ensure contractor table mapping (already annotated, but explicit here)
            modelBuilder.Entity<Contractor>().ToTable("contractor");
        }
    }
}
