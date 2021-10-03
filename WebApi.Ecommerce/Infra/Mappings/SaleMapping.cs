using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Ecommerce.Domain.Entities;

namespace WebApi.Ecommerce.Infra.Mappings
{
    public class SaleMapping
    {
        public SaleMapping(EntityTypeBuilder<Sale> entityBuilder)
        {
            entityBuilder.ToTable("Sales");
            entityBuilder.HasKey(t => t.Id).HasName("Pk_Sales_Id"); ;
            entityBuilder.Property(t => t.CreatedAt).IsRequired().HasColumnType("DATETIME");
            entityBuilder.Property(t => t.UpdatedAt).HasColumnType("DATETIME");
            entityBuilder.Property(t => t.Active).IsRequired().HasColumnType("BIT").HasDefaultValueSql("1");

            entityBuilder.HasOne(c => c.Customer)
                .WithMany(s => s.Sales)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(st => st.SaleType)
                .WithMany(s => s.Sales)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
