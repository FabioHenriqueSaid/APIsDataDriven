using ApiDataDriven.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiDataDriven.Api.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .Property(o => o.Price).HasColumnType("decimal(5,3)");
            base.OnModelCreating(modelBuilder);
        }
    }
}