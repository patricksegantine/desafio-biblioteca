using Basis.Biblioteca.Domain.Entities;
using Basis.Biblioteca.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Basis.Biblioteca.Infrastructure.Persistence.SqlServer.Repositories;

public class AutorRepository(BibliotecaContext context) : IAutorRepository
{
    private readonly BibliotecaContext _context = context;

    public async Task<Autor> AddAsync(Autor autor, CancellationToken cancellationToken = default)
    {
        await _context.Autores.AddAsync(autor, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return autor;
    }

    public async Task<IEnumerable<Autor>> GetAllAsync(string? nome, CancellationToken cancellationToken = default)
    {
        return await _context.Autores
            .AsNoTracking()
            .Where(autor => string.IsNullOrWhiteSpace(nome) || autor.Nome.Contains(nome))
            .ToListAsync(cancellationToken);
    }

    public async Task<Autor?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Autores.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Autor>> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken = default)
    {
        return await _context.Autores.Where(a => ids.Contains(a.CodAu)).ToListAsync(cancellationToken);
    }

    public async Task<Autor> UpdateAsync(Autor autor, CancellationToken cancellationToken = default)
    {
        _context.Autores.Update(autor);
        await _context.SaveChangesAsync(cancellationToken);
        return autor;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var autor = await _context.Autores.FindAsync(id, cancellationToken);
        if (autor == null)
        {
            return false;
        }

        _context.Autores.Remove(autor);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
