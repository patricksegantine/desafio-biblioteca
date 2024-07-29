namespace Basis.Biblioteca.Domain.Entities;

public class Livro : AuditableEntity
{
    public int CodL { get; set; }
    public string Titulo { get; set; } = default!;
    public string Editora { get; set; } = default!;
    public int Edicao { get; set; }
    public string AnoPublicacao { get; set; } = default!;

    public List<Autor> Autores { get; set; } = [];
    public List<Assunto> Assuntos { get; set; } = [];
    public List<PrecoVenda> Precos { get; set; } = [];

    public void Update(string? titulo, string? editora, int? edicao, string? anoPublicacao)
    {
        if (!string.IsNullOrWhiteSpace(titulo))
            Titulo = titulo;

        if (!string.IsNullOrWhiteSpace(editora))
            Editora = editora;

        if (!string.IsNullOrWhiteSpace(anoPublicacao))
            AnoPublicacao = anoPublicacao;

        Edicao = edicao ?? Edicao;
    }
}
