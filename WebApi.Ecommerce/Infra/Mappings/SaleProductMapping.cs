using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Ecommerce.Domain.Entities;

namespace WebApi.Ecommerce.Infra.Mappings
{
    public class SaleProductMapping
    {
        public SaleProductMapping(EntityTypeBuilder<SaleProduct> entityBuilder)
        {
            entityBuilder.ToTable("SaleProducts");
            entityBuilder.HasKey(t => t.Id).HasName("Pk_SaleProducts_Id"); ;
            entityBuilder.Property(t => t.CreatedAt).IsRequired().HasColumnType("TIMESTAMP");
            entityBuilder.Property(t => t.UpdatedAt).HasColumnType("TIMESTAMP");
            entityBuilder.Property(t => t.Active).IsRequired().HasDefaultValueSql("TRUE");

            entityBuilder.HasKey(sp => new { sp.SalesId, sp.ProductId });

            entityBuilder.HasOne(s => s.Sales)
                .WithMany(sp => sp.SaleProducts)
                .HasForeignKey(f => f.SalesId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(p => p.Products)
                .WithMany(sp => sp.SaleProducts)
                .HasForeignKey(f => f.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
