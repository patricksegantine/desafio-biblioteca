namespace Basis.Biblioteca.Domain.Entities;

public class Assunto : AuditableEntity
{
    public int CodAs { get; set; }
    public string Descricao { get; set; } = default!;
    public List<Livro> Livros { get; set; } = [];
}
