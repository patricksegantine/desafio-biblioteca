using Basis.Biblioteca.Domain.DomainObjects.Filters;
using Basis.Biblioteca.Domain.Entities;
using Basis.Biblioteca.Domain.Queries;

namespace Basis.Biblioteca.Domain.Repositories;

public interface ILivroRepository
{
    Task<Livro> AddAsync(Livro livro, CancellationToken cancellationToken = default);
    Task<(int totalRecords, IEnumerable<LivroSummaryDto>)> GetAllAsync(ILivroFilter filter, CancellationToken cancellationToken = default);
    Task<Livro?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Livro> UpdateAsync(Livro livro, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}