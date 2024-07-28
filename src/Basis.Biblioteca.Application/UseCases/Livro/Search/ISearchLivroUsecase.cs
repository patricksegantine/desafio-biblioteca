using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Livro.Search;

public interface ISearchLivroUsecase
{
    Task<ErrorOr<SearchLivroResult>> Handle(SearchLivroRequest request, CancellationToken cancellationToken = default);
}
