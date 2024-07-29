using Basis.Biblioteca.Application.Common.DTOs;

namespace Basis.Biblioteca.Application.UseCases.Livro.Update;

public record UpdateLivroRequest
{
    public int CodL { get; set; }
    public string? Titulo { get; set; }
    public string? Editora { get; set; }
    public int? Edicao { get; set; }
    public string? AnoPublicacao { get; set; }

    public List<int>? Autores { get; set; }
    public List<int>? Assuntos { get; set; }
    public List<PrecoVendaDto>? Precos { get; set; }
}
