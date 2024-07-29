using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Livro.Update;

public interface IUpdateLivroUsecase
{
    Task<ErrorOr<Updated>> Handle(UpdateLivroRequest request, CancellationToken cancellationToken = default);
}