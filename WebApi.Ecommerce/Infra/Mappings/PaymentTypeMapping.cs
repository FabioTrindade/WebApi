using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Ecommerce.Domain.Entities;

namespace WebApi.Ecommerce.Infra.Mappings
{
    public class PaymentTypeMapping
    {
        public PaymentTypeMapping(EntityTypeBuilder<PaymentType> entityBuilder)
        {
            entityBuilder.ToTable("PaymentTypes");
            entityBuilder.HasKey(t => t.Id).HasName("Pk_PaymentTypes_Id"); ;
            entityBuilder.Property(t => t.CreatedAt).IsRequired().HasColumnType("TIMESTAMP");
            entityBuilder.Property(t => t.UpdatedAt).HasColumnType("TIMESTAMP");
            entityBuilder.Property(t => t.Active).IsRequired().HasDefaultValueSql("TRUE");
            entityBuilder.Property(t => t.Description).IsRequired().HasColumnType("VARCHAR(100)");
            entityBuilder.Property(t => t.IsCreditCard).IsRequired().HasDefaultValueSql("FALSE");
        }
    }
}
