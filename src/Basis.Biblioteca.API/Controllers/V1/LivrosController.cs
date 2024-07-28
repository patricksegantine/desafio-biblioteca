using Basis.Biblioteca.API.Presentation;
using Basis.Biblioteca.Application.UseCases.Livro.Search;
using Microsoft.AspNetCore.Mvc;

namespace Basis.Biblioteca.API.Controllers.V1;

[ApiController]
[Route("[controller]")]
public class LivrosController(
    ISearchLivroUsecase searchLivroUsecase,
    ILogger<LivrosController> logger) : BaseController
{
    private readonly ISearchLivroUsecase _searchLivroUsecase = searchLivroUsecase;
    private readonly ILogger<LivrosController> _logger = logger;

    /// <summary>
    /// Pesquisa livros por titulo, assunto, editora, ano de publicação, autor ou assunto
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet(Name = "SearchLivro")]
    [ProducesResponseType(typeof(ApiBaseResponse<SearchLivroResult>), StatusCodes.Status200OK)]
    public async Task<ActionResult<SearchLivroResult>> SearchAutor(
        [FromQuery] SearchLivroRequest request,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _searchLivroUsecase.Handle(request, cancellationToken));
    }
}
