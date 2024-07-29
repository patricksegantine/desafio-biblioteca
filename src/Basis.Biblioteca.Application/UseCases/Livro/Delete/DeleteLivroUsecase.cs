using Basis.Biblioteca.Domain;
using Basis.Biblioteca.Domain.Repositories;
using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Livro.Delete;

public sealed class DeleteLivroUsecase(ILivroRepository livroRepository) : IDeleteLivroUsecase
{
    private readonly ILivroRepository _livroRepository = livroRepository;

    public async Task<ErrorOr<Deleted>> Handle(int codl, CancellationToken cancellationToken = default)
    {
        var livro = await _livroRepository.GetByIdAsync(codl, cancellationToken);
        if (livro is null)
            return ErrorCatalog.NotFound;

        // Regras de validação aqui

        await _livroRepository.DeleteAsync(codl, cancellationToken);

        return Result.Deleted;
    }
}