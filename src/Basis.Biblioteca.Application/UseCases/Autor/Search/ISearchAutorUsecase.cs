using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Autor.Search;

public interface ISearchAutorUsecase
{
    Task<ErrorOr<SearchAutorResult>> Handle(SearchAutorRequest request, CancellationToken cancellationToken = default);
}
