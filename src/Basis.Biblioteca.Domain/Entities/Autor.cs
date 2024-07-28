namespace Basis.Biblioteca.Domain.Entities;

public class Autor : AuditableEntity
{
    public int CodAu { get; set; }
    public string Nome { get; set; } = default!;
    public List<Livro> Livros { get; set; } = [];
}
