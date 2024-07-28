using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Livro.Create;

public sealed class CreateLivroUsecase : ICreateLivroUsecase
{
    public Task<ErrorOr<CreateLivroResult>> Handle(CreateLivroRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}