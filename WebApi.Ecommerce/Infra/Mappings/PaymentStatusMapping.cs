using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Ecommerce.Domain.Entities;

namespace WebApi.Ecommerce.Infra.Mappings
{
    public class PaymentStatusMapping
    {
        public PaymentStatusMapping(EntityTypeBuilder<PaymentStatus> entityBuilder)
        {
            entityBuilder.ToTable("PaymentStatus");
            entityBuilder.HasKey(t => t.Id).HasName("Pk_PaymentStatus_Id"); ;
            entityBuilder.Property(t => t.CreatedAt).IsRequired().HasColumnType("TIMESTAMP");
            entityBuilder.Property(t => t.UpdatedAt).HasColumnType("TIMESTAMP");
            entityBuilder.Property(t => t.Active).IsRequired().HasDefaultValueSql("TRUE");
            entityBuilder.Property(t => t.Description).IsRequired().HasColumnType("VARCHAR(200)");
            entityBuilder.Property(t => t.PaymentStatusId).IsRequired().HasColumnType("INT");

            entityBuilder.HasIndex(i => i.PaymentStatusId).IsUnique();
        }
    }
}
