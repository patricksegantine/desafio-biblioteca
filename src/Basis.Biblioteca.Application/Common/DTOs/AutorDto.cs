namespace Basis.Biblioteca.Application.Common.DTOs;

public record AutorDto
{
    public int CodAu { get; set; }
    public string Nome { get; set; } = default!;
}
