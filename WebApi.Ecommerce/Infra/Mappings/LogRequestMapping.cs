using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Ecommerce.Domain.Entities;

namespace WebApi.Ecommerce.Infra.Mappings
{
    public class LogRequestMapping
    {
        public LogRequestMapping(EntityTypeBuilder<LogRequest> entityBuilder)
        {
            entityBuilder.ToTable("LogRequests");
            entityBuilder.HasKey(t => t.Id).HasName("Pk_LogRequests_Id"); ;
            entityBuilder.Property(t => t.CreatedAt).IsRequired().HasColumnType("DATETIME");
            entityBuilder.Property(t => t.UpdatedAt).HasColumnType("DATETIME");
            entityBuilder.Property(t => t.Active).IsRequired().HasColumnType("BIT").HasDefaultValueSql("1");
            entityBuilder.Property(t => t.Device).HasColumnType("VARCHAR(600)");
            entityBuilder.Property(t => t.Host).HasColumnType("VARCHAR(600)");
            entityBuilder.Property(t => t.Method).HasColumnType("VARCHAR(600)");
            entityBuilder.Property(t => t.Path).HasColumnType("VARCHAR(600)");
            entityBuilder.Property(t => t.Url).HasColumnType("VARCHAR(600)");
            entityBuilder.Property(t => t.Query).HasColumnType("TEXT");
            entityBuilder.Property(t => t.Header).HasColumnType("TEXT");
            entityBuilder.Property(t => t.Body).HasColumnType("TEXT");
            entityBuilder.Property(t => t.Ip).HasColumnType("VARCHAR(200)");
            entityBuilder.Property(t => t.StatusCode).HasColumnType("INT");
            entityBuilder.Property(t => t.Response).HasColumnType("TEXT");
            entityBuilder.Property(t => t.ExecutionTime).HasColumnType("TIME");
        }
    }
}
