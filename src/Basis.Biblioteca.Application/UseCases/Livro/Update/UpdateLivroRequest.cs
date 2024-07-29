using Basis.Biblioteca.Application.Common.DTOs;

namespace Basis.Biblioteca.Application.UseCases.Livro.Update;

public record UpdateLivroRequest
{
    public int CodL { get; set; }
    public string? Titulo { get; set; }
    public string? Editora { get; set; }
    public int? Edicao { get; set; }
    public string? AnoPublicacao { get; set; }

    public List<UpdateAutorDto>? Autores { get; set; }
    public List<UpdateAssuntoDto>? Assuntos { get; set; }
    public List<UpdatePrecoVendaDto>? Precos { get; set; }
}

public record UpdateAutorDto
{
    public int CodAu { get; set; }
    public UpdateActionType UpdateAction { get; set; }
}

public record UpdateAssuntoDto
{
    public int CodAs { get; set; }
    public UpdateActionType UpdateAction { get; set; }
}

public record UpdatePrecoVendaDto: PrecoVendaDto
{
    public UpdateActionType UpdateAction { get; set; }
}

public enum UpdateActionType
{
    Added = 0,
    Deleted = 1
}
