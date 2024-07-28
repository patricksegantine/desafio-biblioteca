using Basis.Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basis.Biblioteca.Infrastructure.Persistence.SqlServer.Configurations;

public class AutorConfiguration : IEntityTypeConfiguration<Autor>
{
    public void Configure(EntityTypeBuilder<Autor> builder)
    {
        builder.ToTable(nameof(Autor));
        builder.HasKey(a => a.CodAu);
        builder.Property(a => a.Nome).IsRequired().HasMaxLength(40);
        builder.Property(a => a.DataCriacao).IsRequired();
        builder.Property(a => a.DataAtualizacao).IsRequired(false);
    }
}

