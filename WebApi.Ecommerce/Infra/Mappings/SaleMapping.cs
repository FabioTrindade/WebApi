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
            entityBuilder.Property(t => t.CreatedAt).IsRequired().HasColumnType("TIMESTAMP");
            entityBuilder.Property(t => t.UpdatedAt).HasColumnType("TIMESTAMP");
            entityBuilder.Property(t => t.Active).IsRequired().HasDefaultValueSql("TRUE");
            entityBuilder.Property(t => t.CreditCard).HasColumnType("VARCHAR(20)");
            entityBuilder.Property(t => t.VerificationCode).HasColumnType("VARCHAR(5)");
            entityBuilder.Property(t => t.ValidityMonth).HasColumnType("VARCHAR(2)");
            entityBuilder.Property(t => t.ValidityYear).HasColumnType("VARCHAR(4)");
            entityBuilder.Property(t => t.CreditCardName).HasColumnType("VARCHAR(200)");
            entityBuilder.Property(t => t.Transaction).HasColumnType("VARCHAR(50)");

            entityBuilder.HasOne(c => c.Customer)
                .WithMany(s => s.Sales)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(pt => pt.PaymentType)
                .WithMany(s => s.Sales)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(ps => ps.PaymentStatus)
                .WithMany(s => s.Sales)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
