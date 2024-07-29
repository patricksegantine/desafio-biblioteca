using Basis.Biblioteca.Domain.Entities;
using Basis.Biblioteca.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Basis.Biblioteca.Infrastructure.Persistence.SqlServer.Repositories;

public class AssuntoRepository(BibliotecaContext context) : IAssuntoRepository
{
    private readonly BibliotecaContext _context = context;

    public async Task<Assunto> AddAsync(Assunto assunto, CancellationToken cancellationToken = default)
    {
        await _context.Assuntos.AddAsync(assunto, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return assunto;
    }

    public async Task<IEnumerable<Assunto>> GetAllAsync(string? descricao, CancellationToken cancellationToken = default)
    {
        return await _context.Assuntos
            .AsQueryable()
            .Where(assunto => string.IsNullOrWhiteSpace(descricao) || assunto.Descricao.Contains(descricao))
            .ToListAsync(cancellationToken);
    }

    public async Task<Assunto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Assuntos.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Assunto>> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken = default)
    {
        return await _context.Assuntos.Where(a => ids.Contains(a.CodAs)).ToListAsync(cancellationToken);
    }

    public async Task<Assunto> UpdateAsync(Assunto assunto, CancellationToken cancellationToken = default)
    {
        _context.Assuntos.Update(assunto);
        await _context.SaveChangesAsync(cancellationToken);
        return assunto;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var assunto = await _context.Assuntos.FindAsync(id, cancellationToken);
        if (assunto == null)
        {
            return false;
        }

        _context.Assuntos.Remove(assunto);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
