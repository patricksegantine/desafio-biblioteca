using Basis.Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basis.Biblioteca.Infrastructure.Persistence.SqlServer.Configurations;

public class LivroConfiguration : IEntityTypeConfiguration<Livro>
{
    public void Configure(EntityTypeBuilder<Livro> builder)
    {
        builder.ToTable(nameof(Livro));
        builder.HasKey(l => l.CodL);
        builder.Property(l => l.Titulo).IsRequired().HasMaxLength(40);
        builder.Property(l => l.Editora).IsRequired().HasMaxLength(40);
        builder.Property(l => l.Edicao).IsRequired();
        builder.Property(l => l.AnoPublicacao).IsRequired().HasMaxLength(4);
        builder.Property(a => a.DataCriacao).IsRequired();
        builder.Property(a => a.DataAtualizacao).IsRequired(false);

        builder.HasMany(l => l.Autores)
               .WithMany(a => a.Livros)
               .UsingEntity<Dictionary<string, object>>(
                   "Livro_Autor",
                   j => j.HasOne<Autor>().WithMany().HasForeignKey("CodAu"),
                   j => j.HasOne<Livro>().WithMany().HasForeignKey("CodL"));

        builder.HasMany(l => l.Assuntos)
               .WithMany(a => a.Livros)
               .UsingEntity<Dictionary<string, object>>(
                   "Livro_Assunto",
                   j => j.HasOne<Assunto>().WithMany().HasForeignKey("CodAs"),
                   j => j.HasOne<Livro>().WithMany().HasForeignKey("CodL"));

        builder.HasMany(l => l.Precos)
               .WithOne()
               .HasForeignKey("CodL");
    }
}
