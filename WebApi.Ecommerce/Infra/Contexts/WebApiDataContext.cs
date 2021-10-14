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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                var host = _configuration["DBHOST"] ?? "localhost";
                var password = _configuration["DBPASSWORD"] ?? "p0stGr3s";

                optionsBuilder
                    .UseNpgsql(string.Format(_configuration.GetConnectionString("WebApiConnection"), host, password), m => m.MigrationsHistoryTable("WebApiEcommerceMigrations"))
                    .UseLowerCaseNamingConvention();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapForgottenProperty(modelBuilder);
            base.OnModelCreating(modelBuilder);

            new CustomerMapping(modelBuilder.Entity<Customer>());
            new ProductMapping(modelBuilder.Entity<Product>());
            new SaleMapping(modelBuilder.Entity<Sale>());
            new SaleProductMapping(modelBuilder.Entity<SaleProduct>());
            new LogRequestMapping(modelBuilder.Entity<LogRequest>());
            new LogErroMapping(modelBuilder.Entity<LogErro>());
            new PaymentTypeMapping(modelBuilder.Entity<PaymentType>());
            new PaymentStatusMapping(modelBuilder.Entity<PaymentStatus>());
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
