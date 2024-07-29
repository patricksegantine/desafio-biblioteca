using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Livro.Delete;

public interface IDeleteLivroUsecase
{
    Task<ErrorOr<Deleted>> Handle(int codl, CancellationToken cancellationToken = default);
}
