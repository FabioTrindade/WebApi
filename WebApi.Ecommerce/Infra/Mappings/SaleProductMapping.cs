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
            entityBuilder.Property(t => t.Quantity).IsRequired().HasColumnType("INT");
            entityBuilder.Property(t => t.Sale).IsRequired(false).HasColumnType("DECIMAL(19, 4)");

            entityBuilder.HasKey(sp => new { sp.SaleId, sp.ProductId });

            entityBuilder.HasOne(s => s.Sales)
                .WithMany(sp => sp.SaleProducts)
                .HasForeignKey(f => f.SaleId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(p => p.Products)
                .WithMany(sp => sp.SaleProducts)
                .HasForeignKey(f => f.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
