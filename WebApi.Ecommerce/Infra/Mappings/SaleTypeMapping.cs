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
            entityBuilder.Property(t => t.CreatedAt).IsRequired().HasColumnType("DATETIME");
            entityBuilder.Property(t => t.UpdatedAt).HasColumnType("DATETIME");
            entityBuilder.Property(t => t.Active).IsRequired().HasColumnType("BIT").HasDefaultValueSql("1");
            entityBuilder.Property(t => t.Description).IsRequired().HasColumnType("VARCHAR(100)");
        }
    }
}
