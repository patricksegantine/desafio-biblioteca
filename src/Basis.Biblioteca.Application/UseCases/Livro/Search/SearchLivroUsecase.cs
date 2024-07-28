using Basis.Biblioteca.Application.Common.DTOs;
using Basis.Biblioteca.Domain.DomainObjects.Filters;
using Basis.Biblioteca.Domain.Queries;
using Basis.Biblioteca.Domain.Repositories;
using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Livro.Search;

public record SearchLivroRequest : ILivroFilter
{
    public string? Titulo { get; set; }
    public string? Assunto { get; set; }
    public string? Autor { get; set; }
    public string? Editora { get; set; }
    public string? AnoPublicacao { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}

public class SearchLivroResult(long total, IEnumerable<LivroSummaryDto> items) 
    : PagedResultDto<LivroSummaryDto>(total, items)
{
}

public sealed class SearchLivroUsecase(ILivroRepository livroRepository) : ISearchLivroUsecase
{
    private readonly ILivroRepository _livroRepository = livroRepository;

    public async Task<ErrorOr<SearchLivroResult>> Handle(SearchLivroRequest request, CancellationToken cancellationToken = default)
    {
        var (totalRecords, livros) = await _livroRepository.GetAllAsync(request, cancellationToken);

        return new SearchLivroResult(totalRecords, livros);
    }
}