using Basis.Biblioteca.Domain.DomainObjects;

namespace Basis.Biblioteca.Domain.Entities;

public class PrecoVenda : AuditableEntity
{
    public int CodP { get; set; }
    public TipoDeVendaType TipoDeVenda { get; set; }
    public decimal Preco { get; set; }
}
