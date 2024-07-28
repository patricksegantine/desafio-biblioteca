namespace Basis.Biblioteca.Domain.Queries;

public record LivroSummaryDto
{
    public int CodL { get; set; }
    public string Titulo { get; set; } = default!;
    public string Editora { get; set; } = default!;
    public int Edicao { get; set; }
    public string AnoPublicacao { get; set; } = default!;
}
