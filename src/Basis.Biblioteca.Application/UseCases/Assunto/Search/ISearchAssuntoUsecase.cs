using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Assunto.Search;

public interface ISearchAssuntoUsecase
{
    Task<ErrorOr<SearchAssuntoResult>> Handle(SearchAssuntoRequest request, CancellationToken cancellationToken = default);
}
