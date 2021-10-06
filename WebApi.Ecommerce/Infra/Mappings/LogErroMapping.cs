using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Ecommerce.Domain.Entities;

namespace WebApi.Ecommerce.Infra.Mappings
{
    public class LogErroMapping
    {
        public LogErroMapping(EntityTypeBuilder<LogErro> entityBuilder)
        {
            entityBuilder.ToTable("LogErros");
            entityBuilder.HasKey(t => t.Id).HasName("Pk_LogErros_Id"); ;
            entityBuilder.Property(t => t.CreatedAt).IsRequired().HasColumnType("TIMESTAMP");
            entityBuilder.Property(t => t.UpdatedAt).HasColumnType("TIMESTAMP");
            entityBuilder.Property(t => t.Active).IsRequired().HasDefaultValueSql("TRUE");
            entityBuilder.Property(t => t.Method).HasColumnType("VARCHAR(100)");
            entityBuilder.Property(t => t.Path).HasColumnType("VARCHAR(200)");
            entityBuilder.Property(t => t.Erro).HasColumnType("VARCHAR(8000)");
            entityBuilder.Property(t => t.ErroCompleto).HasColumnType("VARCHAR(8000)");
            entityBuilder.Property(t => t.Query).HasColumnType("VARCHAR(8000)");
        }
    }
}
