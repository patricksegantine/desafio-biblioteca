using Basis.Biblioteca.Application.Common.DTOs;

namespace Basis.Biblioteca.Application.UseCases.Livro.Create;

public record CreateLivroRequest
{
    public string Titulo { get; set; } = default!;
    public string Editora { get; set; } = default!;
    public int Edicao { get; set; }
    public string AnoPublicacao { get; set; } = default!;

    public List<int> Autores { get; set; } = [];
    public List<int> Assuntos { get; set; } = [];
    public List<PrecoVendaDto> Precos { get; set; } = [];
}
