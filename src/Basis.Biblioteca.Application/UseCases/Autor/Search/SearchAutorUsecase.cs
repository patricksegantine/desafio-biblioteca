using Basis.Biblioteca.Domain.Repositories;
using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Autor.Search;

public record SearchAutorRequest
{
    public string? Nome { get; set; }
}

public class SearchAutorResult : List<Domain.Entities.Autor>
{
    public SearchAutorResult(IEnumerable<Domain.Entities.Autor> autores)
    {
        AddRange(autores);
    }
}

public sealed class SearchAutorUsecase(IAutorRepository autorRepository) : ISearchAutorUsecase
{
    private readonly IAutorRepository _autorRepository = autorRepository;

    public async Task<ErrorOr<SearchAutorResult>> Handle(SearchAutorRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _autorRepository.GetAllAsync(request.Nome, cancellationToken);

        return new SearchAutorResult(result);
    }
}
