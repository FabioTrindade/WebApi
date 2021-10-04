using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Ecommerce.Domain.Entities;

namespace WebApi.Ecommerce.Infra.Mappings
{
    public class SaleTypeMapping
    {
        public SaleTypeMapping(EntityTypeBuilder<SaleType> entityBuilder)
        {
            entityBuilder.ToTable("SaleTypes");
            entityBuilder.HasKey(t => t.Id).HasName("Pk_SaleTypes_Id"); ;
            entityBuilder.Property(t => t.CreatedAt).IsRequired().HasColumnType("TIMESTAMP");
            entityBuilder.Property(t => t.UpdatedAt).HasColumnType("TIMESTAMP");
            entityBuilder.Property(t => t.Active).IsRequired().HasDefaultValueSql("TRUE");
            entityBuilder.Property(t => t.Description).IsRequired().HasColumnType("VARCHAR(100)");
        }
    }
}
