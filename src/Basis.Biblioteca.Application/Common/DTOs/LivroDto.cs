namespace Basis.Biblioteca.Application.Common.DTOs;

public abstract record LivroDto
{
    public int CodL { get; set; }
    public string Titulo { get; set; } = default!;
    public string Editora { get; set; } = default!;
    public int Edicao { get; set; }
    public string AnoPublicacao { get; set; } = default!;

    public List<AutorDto> Autores { get; set; } = [];
    public List<AssuntoDto> Assuntos { get; set; } = [];
    public List<PrecoVendaDto> Precos { get; set; } = [];
}
