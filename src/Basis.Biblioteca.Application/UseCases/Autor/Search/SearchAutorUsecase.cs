using Basis.Biblioteca.Application.Common.DTOs;
using Basis.Biblioteca.Domain.Repositories;
using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Autor.Search;

public record SearchAutorRequest
{
    public string? Nome { get; set; }
}

public class SearchAutorResult : List<AutorDto>
{
    public SearchAutorResult(IEnumerable<Domain.Entities.Autor> autores)
    {
        foreach (var autor in autores)
            Add(new AutorDto { CodAu = autor.CodAu, Nome = autor.Nome });
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
