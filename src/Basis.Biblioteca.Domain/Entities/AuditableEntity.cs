namespace Basis.Biblioteca.Domain.Entities;

public abstract class AuditableEntity
{
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
}
