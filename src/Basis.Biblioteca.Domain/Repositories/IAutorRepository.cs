using Basis.Biblioteca.Domain.Entities;

namespace Basis.Biblioteca.Domain.Repositories;

public interface IAutorRepository
{
    Task<Autor> AddAsync(Autor autor, CancellationToken cancellationToken = default);
    Task<IEnumerable<Autor>> GetAllAsync(string? nome, CancellationToken cancellationToken = default);
    Task<Autor?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Autor> UpdateAsync(Autor autor, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
