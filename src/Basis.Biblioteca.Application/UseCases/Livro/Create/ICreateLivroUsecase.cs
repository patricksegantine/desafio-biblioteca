using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Livro.Create;

public interface ICreateLivroUsecase
{
    Task<ErrorOr<CreateLivroResult>> Handle(CreateLivroRequest request, CancellationToken cancellationToken = default);
}
