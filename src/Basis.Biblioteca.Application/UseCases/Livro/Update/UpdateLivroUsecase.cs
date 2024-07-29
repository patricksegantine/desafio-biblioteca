using Basis.Biblioteca.Application.Common.Extensions;
using Basis.Biblioteca.Application.Common.Validators;
using Basis.Biblioteca.Domain;
using Basis.Biblioteca.Domain.Entities;
using Basis.Biblioteca.Domain.Repositories;
using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Livro.Update;

public sealed class UpdateLivroUsecase(
    ILivroRepository livroRepository,
    IAutorRepository autorRepository,
    IAssuntoRepository assuntoRepository) : IUpdateLivroUsecase
{
    private readonly ILivroRepository _livroRepository = livroRepository;
    private readonly IAutorRepository _autorRepository = autorRepository;
    private readonly IAssuntoRepository _assuntoRepository = assuntoRepository;

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
        livro = ProcessDeletedAutores(livro, request.Autores);

        // Validar Assuntos
        livro = ProcessDeletedAssuntos(livro, request.Assuntos);

        // Validar Precos
        livro = ProcessDeletedPrecos(livro, request.Precos);

        await _livroRepository.UpdateAsync(livro, cancellationToken);

        return Result.Updated;
    }

    private static Domain.Entities.Livro ProcessDeletedAutores(Domain.Entities.Livro livro, List<UpdateAutorDto>? autores)
    {
        if (autores is null || autores.Count == 0)
            return livro;

        var deletedAutores = autores.Where(a => a.UpdateAction == UpdateActionType.Deleted)
                                    .Select(a => a.CodAu)
                                    .ToList();

        foreach (var codAu in deletedAutores)
        {
            var autor = livro.Autores.Find(a => a.CodAu == codAu);
            if (autor is null)
                continue;

            livro.Autores.Remove(autor);
        }

        return livro;
    }

    private Domain.Entities.Livro ProcessDeletedAssuntos(Domain.Entities.Livro livro, List<UpdateAssuntoDto>? assuntos)
    {
        if (assuntos is null || assuntos.Count == 0)
            return livro;

        var deletedAssuntos = assuntos.Where(a => a.UpdateAction == UpdateActionType.Deleted)
                                    .Select(a => a.CodAs)
                                    .ToList();

        foreach (var codAs in deletedAssuntos)
        {
            var assunto = livro.Assuntos.Find(a => a.CodAs == codAs);
            if (assunto is null)
                continue;

            livro.Assuntos.Remove(assunto);
        }

        return livro;
    }

    private Domain.Entities.Livro ProcessDeletedPrecos(Domain.Entities.Livro livro, List<UpdatePrecoVendaDto>? precos)
    {
        if (precos is null || precos.Count == 0)
            return livro;

        var deletedPrecos = precos.Where(a => a.UpdateAction == UpdateActionType.Deleted)
                                    .Select(a => a.CodP)
                                    .ToList();

        foreach (var codP in deletedPrecos)
        {
            var preco = livro.Precos.Find(a => a.CodP == codP);
            if (preco is null)
                continue;

            livro.Precos.Remove(preco);
        }

        return livro;
    }
}
