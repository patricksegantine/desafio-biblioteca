namespace Basis.Biblioteca.Domain.DomainObjects.Filters;

public interface ILivroFilter
{
    public string? Titulo { get; set; }
    public string? Assunto { get; set; }
    public string? Autor { get; set; }
    public string? Editora { get; set; }
    public string? AnoPublicacao { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
