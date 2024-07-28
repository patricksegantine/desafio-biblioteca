using Basis.Biblioteca.Domain.Repositories;
using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Assunto.Search;

public record SearchAssuntoRequest
{
    public string? Nome { get; set; }
}

public class SearchAssuntoResult : List<Domain.Entities.Assunto>
{
    public SearchAssuntoResult(IEnumerable<Domain.Entities.Assunto> assuntos)
    {
        AddRange(assuntos);
    }
}

public sealed class SearchAssuntoUsecase(IAssuntoRepository assuntoRepository) : ISearchAssuntoUsecase
{
    private readonly IAssuntoRepository _assuntoRepository = assuntoRepository;

    public async Task<ErrorOr<SearchAssuntoResult>> Handle(SearchAssuntoRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _assuntoRepository.GetAllAsync(request.Nome, cancellationToken);

        return new SearchAssuntoResult(result);
    }
}
