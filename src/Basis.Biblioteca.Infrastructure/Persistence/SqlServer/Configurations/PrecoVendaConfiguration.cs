using Basis.Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basis.Biblioteca.Infrastructure.Persistence.SqlServer.Configurations;

public class PrecoVendaConfiguration : IEntityTypeConfiguration<PrecoVenda>
{
    public void Configure(EntityTypeBuilder<PrecoVenda> builder)
    {
        builder.ToTable(nameof(PrecoVenda));
        builder.HasKey(p => p.CodP);
        builder.Property(p => p.TipoDeVenda).IsRequired();
        builder.Property(p => p.Preco).IsRequired().HasPrecision(9, 2);
        builder.Property(a => a.DataCriacao).IsRequired();
        builder.Property(a => a.DataAtualizacao).IsRequired(false);
    }
}

