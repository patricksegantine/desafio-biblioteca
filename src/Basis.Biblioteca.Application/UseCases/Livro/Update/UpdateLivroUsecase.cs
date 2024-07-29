using Basis.Biblioteca.Application.Common.Extensions;
using Basis.Biblioteca.Application.Common.Validators;
using Basis.Biblioteca.Domain;
using Basis.Biblioteca.Domain.Repositories;
using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Livro.Update;

public sealed class UpdateLivroUsecase(ILivroRepository livroRepository) : IUpdateLivroUsecase
{
    private readonly ILivroRepository _livroRepository = livroRepository;

    public async Task<ErrorOr<Updated>> Handle(UpdateLivroRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = await new UpdateLivroValidator().ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.Errors.ToErrorList();

        var livro = await _livroRepository.GetByIdAsync(request.CodL, cancellationToken);
        if (livro is null)
            return ErrorCatalog.NotFound;

        livro.Update(
            request.Titulo,
            request.Editora,
            request.Edicao,
            request.AnoPublicacao
        );

        // Validar Autores

        // Validar Assuntos

        // Validar Precos

        await _livroRepository.UpdateAsync(livro, cancellationToken);

        return Result.Updated;
    }
}
