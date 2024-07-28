using Basis.Biblioteca.Domain.Entities;

namespace Basis.Biblioteca.Domain.Repositories;

public interface IAssuntoRepository
{
    Task<Assunto> AddAsync(Assunto assunto, CancellationToken cancellationToken = default);
    Task<IEnumerable<Assunto>> GetAllAsync(string? descricao, CancellationToken cancellationToken = default);
    Task<Assunto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Assunto> UpdateAsync(Assunto assunto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
