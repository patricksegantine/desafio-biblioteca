using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Livro.Get;

public interface IGetLivroUsecase
{
    Task<ErrorOr<GetLivroResult>> Handle(int codl, CancellationToken cancellationToken = default);
}
