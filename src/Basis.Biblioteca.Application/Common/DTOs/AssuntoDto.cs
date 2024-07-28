namespace Basis.Biblioteca.Application.Common.DTOs;

public record AssuntoDto
{
    public int CodAs { get; set; }
    public string Descricao { get; set; } = default!;
}
