using AutoMapper;
using Basis.Biblioteca.Application.Common.Extensions;
using Basis.Biblioteca.Application.Common.Validators;
using Basis.Biblioteca.Domain.Repositories;
using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Livro.Create;

public sealed class CreateLivroUsecase(
    ILivroRepository livroRepository,
    IMapper mapper) : ICreateLivroUsecase
{
    private readonly ILivroRepository _livroRepository = livroRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ErrorOr<CreateLivroResult>> Handle(CreateLivroRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = await new CreateLivroValidator().ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.Errors.ToErrorList();

        var livro = _mapper.Map<Domain.Entities.Livro>(request);

        livro = await _livroRepository.AddAsync(livro, cancellationToken);

        return _mapper.Map<CreateLivroResult>(livro);
    }
}