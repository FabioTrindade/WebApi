using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Infra.Mappings;

namespace WebApi.Ecommerce.Infra.Contexts
{
    public class WebApiDataContext : DbContext
    {
        public WebApiDataContext()
        {
        }

        private readonly IConfiguration _configuration;

        public WebApiDataContext(DbContextOptions options, IConfiguration configuration) : base(options) {
            _configuration = configuration;
        }

        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<SaleType> SaleTypes { get; set; }
        //public DbSet<Sale> Sales { get; set; }
        //public DbSet<SaleProduct> SaleProducts { get; set; }
        //public DbSet<LogRequest> LogRequests { get; set; }
        //public DbSet<LogErro> LogErros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseNpgsql(_configuration.GetConnectionString("WebApiConnection"), m => m.MigrationsHistoryTable("WebApiEcommerceMigrations"))
                    .UseLowerCaseNamingConvention();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapForgottenProperty(modelBuilder);
            base.OnModelCreating(modelBuilder);

            new CustomerMapping(modelBuilder.Entity<Customer>());
            new ProductMapping(modelBuilder.Entity<Product>());
            new SaleTypeMapping(modelBuilder.Entity<SaleType>());
            new SaleMapping(modelBuilder.Entity<Sale>());
            new SaleProductMapping(modelBuilder.Entity<SaleProduct>());
            new LogRequestMapping(modelBuilder.Entity<LogRequest>());
            new LogErroMapping(modelBuilder.Entity<LogErro>());
        }

        /// <summary>
        /// Mapeamento de propriedades esquecidas
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void MapForgottenProperty(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties().Where(p => p.ClrType == typeof(string));
                foreach (var property in properties)
                {
                    if (string.IsNullOrEmpty(property.GetColumnType()) && !property.GetMaxLength().HasValue)
                    {
                        property.SetColumnType("VARCHAR(100)");
                    }
                }
            }
        }
    }
}
