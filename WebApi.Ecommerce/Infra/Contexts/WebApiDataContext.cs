using Microsoft.EntityFrameworkCore;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Infra.Mappings;

namespace WebApi.Ecommerce.Infra.Contexts
{
    public class WebApiDataContext : DbContext
    {
        public WebApiDataContext()
        {
        }

        public WebApiDataContext(DbContextOptions options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SaleType> SaleTypes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleProduct> SaleProducts { get; set; }
        public DbSet<LogRequest> LogRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("", m => m.MigrationsHistoryTable("WebApiEcommerceMigrations"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new CustomerMapping(modelBuilder.Entity<Customer>());
            new ProductMapping(modelBuilder.Entity<Product>());
            new SaleTypeMapping(modelBuilder.Entity<SaleType>());
            new SaleMapping(modelBuilder.Entity<Sale>());
            new LogRequestMapping(modelBuilder.Entity<LogRequest>());
        }
    }
}
