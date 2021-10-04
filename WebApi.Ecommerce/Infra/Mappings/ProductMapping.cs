using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Ecommerce.Domain.Entities;

namespace WebApi.Ecommerce.Infra.Mappings
{
    public class ProductMapping
    {
        public ProductMapping(EntityTypeBuilder<Product> entityBuilder)
        {
            entityBuilder.ToTable("Products");
            entityBuilder.HasKey(t => t.Id).HasName("Pk_Products_Id"); ;
            entityBuilder.Property(t => t.CreatedAt).IsRequired().HasColumnType("TIMESTAMP");
            entityBuilder.Property(t => t.UpdatedAt).HasColumnType("TIMESTAMP");
            entityBuilder.Property(t => t.Active).IsRequired().HasDefaultValueSql("TRUE");
            entityBuilder.Property(t => t.Description).IsRequired().HasColumnType("VARCHAR(200)");
            entityBuilder.Property(t => t.SKU).IsRequired().HasColumnType("VARCHAR(20)");
            entityBuilder.Property(t => t.Quantity).IsRequired().HasColumnType("INT").HasDefaultValueSql("0");
            entityBuilder.Property(t => t.Sale).IsRequired(false).HasColumnType("DECIMAL(19, 4)");
        }
    }
}
