using Basis.Biblioteca.Domain.DomainObjects.Filters;
using Basis.Biblioteca.Domain.Entities;
using Basis.Biblioteca.Domain.Queries;
using Basis.Biblioteca.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Basis.Biblioteca.Infrastructure.Persistence.SqlServer.Repositories;

public class LivroRepository(BibliotecaContext context) : ILivroRepository
{
    private readonly BibliotecaContext _context = context;

    public async Task<Livro> AddAsync(Livro livro, CancellationToken cancellationToken = default)
    {
        await _context.Livros.AddAsync(livro, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return livro;
    }

    public async Task<(int totalRecords, IEnumerable<LivroSummaryDto>)> GetAllAsync(ILivroFilter filter, CancellationToken cancellationToken = default)
    {
        var pageSize = filter.PageSize ?? 10;
        var toSkip = ((filter.PageNumber ?? 1) - 1) * pageSize;

        var query = _context.Livros
            .Include(l => l.Autores)
            .Include(l => l.Assuntos)
            .Include(l => l.Precos)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filter.Titulo))
            query = query.Where(livro => livro.Titulo.Contains(filter.Titulo));

        if (!string.IsNullOrEmpty(filter.Assunto))
            query = query.Where(l => l.Assuntos.Any(a => a.Descricao.Contains(filter.Assunto)));

        if (!string.IsNullOrEmpty(filter.Autor))
            query = query.Where(l => l.Autores.Any(a => a.Nome.Contains(filter.Autor)));

        if (!string.IsNullOrEmpty(filter.Editora))
            query = query.Where(l => l.Editora.Contains(filter.Editora));

        if (!string.IsNullOrEmpty(filter.AnoPublicacao))
            query = query.Where(l => l.AnoPublicacao.Contains(filter.AnoPublicacao));

        query = query.OrderBy(livro => livro.Titulo);

        var totalCount = await query.CountAsync(cancellationToken);

        var result = await query
            .AsSplitQuery()
            .Skip(toSkip)
            .Take(pageSize)
            .Select(l => new LivroSummaryDto
            {
                CodL = l.CodL,
                Titulo = l.Titulo,
                Editora = l.Editora,
                Edicao = l.Edicao,
                AnoPublicacao = l.AnoPublicacao
            }).ToListAsync(cancellationToken);

        return (totalCount, result);
    }

    public async Task<Livro?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Livros
            .Include(l => l.Autores)
            .Include(l => l.Assuntos)
            .Include(l => l.Precos)
            .AsSplitQuery()
            .FirstOrDefaultAsync(l => l.CodL == id, cancellationToken);
    }

    public async Task<Livro> UpdateAsync(Livro livro, CancellationToken cancellationToken = default)
    {
        _context.Livros.Update(livro);
        await _context.SaveChangesAsync(cancellationToken);
        return livro;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var livro = await _context.Livros.FindAsync(id, cancellationToken);
        if (livro == null)
        {
            return false;
        }

        _context.Livros.Remove(livro);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}