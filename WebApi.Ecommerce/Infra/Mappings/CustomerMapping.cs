using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Ecommerce.Domain.Entities;

namespace WebApi.Ecommerce.Infra.Mappings
{
    public class CustomerMapping
    {
        public CustomerMapping(EntityTypeBuilder<Customer> entityBuilder)
        {
            entityBuilder.ToTable("Customers");
            entityBuilder.HasKey(t => t.Id).HasName("Pk_Customers_Id");
            entityBuilder.Property(t => t.CreatedAt).IsRequired().HasColumnType("TIMESTAMP");
            entityBuilder.Property(t => t.UpdatedAt).HasColumnType("TIMESTAMP");
            entityBuilder.Property(t => t.Active).IsRequired().HasDefaultValueSql("TRUE");
            entityBuilder.Property(t => t.Name).IsRequired().HasColumnType("VARCHAR(200)");
            entityBuilder.Property(t => t.Document).IsRequired().HasColumnType("VARCHAR(20)");
            entityBuilder.Property(t => t.ZipCode).IsRequired().HasColumnType("VARCHAR(10)");
            entityBuilder.Property(t => t.Address).IsRequired().HasColumnType("VARCHAR(200)");
            entityBuilder.Property(t => t.Number).IsRequired().HasColumnType("VARCHAR(10)");
            entityBuilder.Property(t => t.Neighborhood).IsRequired().HasColumnType("VARCHAR(100)");
            entityBuilder.Property(t => t.Complement).IsRequired(false).HasColumnType("VARCHAR(100)");
            entityBuilder.Property(t => t.City).IsRequired().HasColumnType("VARCHAR(100)");
            entityBuilder.Property(t => t.State).IsRequired().HasColumnType("VARCHAR(10)");
            entityBuilder.Property(t => t.Country).IsRequired().HasColumnType("VARCHAR(10)");
            entityBuilder.Property(t => t.CellPhone).IsRequired().HasColumnType("VARCHAR(20)");
            entityBuilder.Property(t => t.Phone).IsRequired(false).HasColumnType("VARCHAR(20)");
            entityBuilder.Property(t => t.Email).IsRequired().HasColumnType("VARCHAR(200)");
        }
    }
}
