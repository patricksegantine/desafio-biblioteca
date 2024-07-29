using AutoMapper;
using Basis.Biblioteca.Domain;
using Basis.Biblioteca.Domain.Repositories;
using ErrorOr;

namespace Basis.Biblioteca.Application.UseCases.Livro.Get;

public sealed class GetLivroUsecase(
    ILivroRepository livroRepository,
    IMapper mapper) : IGetLivroUsecase
{
    private readonly ILivroRepository _livroRepository = livroRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ErrorOr<GetLivroResult>> Handle(int codl, CancellationToken cancellationToken = default)
    {
        var livro = await _livroRepository.GetByIdAsync(codl, cancellationToken);
        if (livro is null)
            return ErrorCatalog.NotFound;

        var result = _mapper.Map<GetLivroResult>(livro);

        return result;
    }
}
