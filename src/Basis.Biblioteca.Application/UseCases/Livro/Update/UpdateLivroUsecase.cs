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

        livro = await ProcessAutores(livro, request.Autores, cancellationToken);

        livro = await ProcessAssuntos(livro, request.Assuntos, cancellationToken);

        livro = ProcessPrecos(livro, request.Precos);


        await _livroRepository.UpdateAsync(livro, cancellationToken);

        return Result.Updated;
    }

    private async Task<Domain.Entities.Livro> ProcessAutores(Domain.Entities.Livro livro, List<UpdateAutorDto>? autores, CancellationToken cancellationToken)
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

        var addedAutoresIds = autores.Where(a => a.UpdateAction == UpdateActionType.Added)
                                    .Select(a => a.CodAu)
                                    .Except(livro.Autores.Select(a => a.CodAu).ToList())
                                    .ToList();
        if (addedAutoresIds.Count > 0)
        {
            var addedAutores = await _autorRepository.GetByIdsAsync(addedAutoresIds, cancellationToken);
            livro.Autores.AddRange(addedAutores);
        }

        return livro;
    }

    private async Task<Domain.Entities.Livro> ProcessAssuntos(Domain.Entities.Livro livro, List<UpdateAssuntoDto>? assuntos, CancellationToken cancellationToken)
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

        var addedAssuntosIds = assuntos.Where(a => a.UpdateAction == UpdateActionType.Added)
                                        .Select(a => a.CodAs)
                                        .Except(livro.Assuntos.Select(a => a.CodAs).ToList())
                                        .ToList();
        if (addedAssuntosIds.Count > 0)
        {
            var addedAssuntos = await _assuntoRepository.GetByIdsAsync(addedAssuntosIds, cancellationToken);
            livro.Assuntos.AddRange(addedAssuntos);
        }

        return livro;
    }

    private Domain.Entities.Livro ProcessPrecos(Domain.Entities.Livro livro, List<UpdatePrecoVendaDto>? precos)
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

        var addedPrecos = precos.Where(a => a.UpdateAction == UpdateActionType.Added)
                                .Select(p => new PrecoVenda
                                {
                                    TipoDeVenda = p.TipoDeVenda,
                                    Preco = p.Preco,
                                })
                                .ToList();
        if (addedPrecos.Count > 0)
        {
            foreach (var addedPreco in addedPrecos)
            {
                var preco = livro.Precos.Find(p => p.TipoDeVenda == addedPreco.TipoDeVenda);
                if (preco is not null)
                {
                    preco.Preco = addedPreco.Preco;
                    continue;
                }

                livro.Precos.Add(addedPreco);
            }
        }

        return livro;
    }

}
