using AutoMapper;
using Basis.Biblioteca.Application.Common.Extensions;
using Basis.Biblioteca.Application.Common.Validators;
using Basis.Biblioteca.Domain.Repositories;
using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Livro.Create;

public sealed class CreateLivroUsecase(
    ILivroRepository livroRepository,
    IAutorRepository autorRepository,
    IAssuntoRepository assuntoRepository,
    IMapper mapper) : ICreateLivroUsecase
{
    private readonly ILivroRepository _livroRepository = livroRepository;
    private readonly IAutorRepository _autorRepository = autorRepository;
    private readonly IAssuntoRepository _assuntoRepository = assuntoRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ErrorOr<CreateLivroResult>> Handle(CreateLivroRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = await new CreateLivroValidator().ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.Errors.ToErrorList();

        var livro = _mapper.Map<Domain.Entities.Livro>(request);

        var autores = await _autorRepository.GetByIdsAsync(request.Autores, cancellationToken);
        livro.Autores = autores.ToList();

        var assuntos = await _assuntoRepository.GetByIdsAsync(request.Assuntos, cancellationToken);
        livro.Assuntos = assuntos.ToList();

        livro = await _livroRepository.AddAsync(livro, cancellationToken);

        return _mapper.Map<CreateLivroResult>(livro);
    }
}