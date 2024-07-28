using Basis.Biblioteca.Domain.DomainObjects;

namespace Basis.Biblioteca.Application.Common.DTOs;

public record PrecoVendaDto
{
    public int? CodP { get; set; }
    public TipoDeVendaType TipoDeVenda { get; set; }
    public decimal Preco { get; set; }
}
