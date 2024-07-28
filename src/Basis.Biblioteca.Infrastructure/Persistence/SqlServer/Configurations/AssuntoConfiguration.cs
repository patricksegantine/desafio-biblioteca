using Basis.Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basis.Biblioteca.Infrastructure.Persistence.SqlServer.Configurations;

public class AssuntoConfiguration : IEntityTypeConfiguration<Assunto>
{
    public void Configure(EntityTypeBuilder<Assunto> builder)
    {
        builder.ToTable(nameof(Assunto));
        builder.HasKey(a => a.CodAs);
        builder.Property(a => a.Descricao).IsRequired().HasMaxLength(20);
        builder.Property(a => a.DataCriacao).IsRequired();
        builder.Property(a => a.DataAtualizacao).IsRequired(false);
    }
}

