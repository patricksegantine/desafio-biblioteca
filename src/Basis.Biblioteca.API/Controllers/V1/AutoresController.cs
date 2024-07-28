using Basis.Biblioteca.API.Presentation;
using Basis.Biblioteca.Application.UseCases.Autor.Search;
using Microsoft.AspNetCore.Mvc;

namespace Basis.Biblioteca.API.Controllers.V1;

[ApiController]
[Route("[controller]")]
public class AutoresController(
    ISearchAutorUsecase searchAutorUsecase,
    ILogger<AutoresController> logger) : BaseController
{
    private readonly ISearchAutorUsecase _searchAutorUsecase = searchAutorUsecase;
    private readonly ILogger<AutoresController> _logger = logger;

    /// <summary>
    /// Pesquisa autores pelo nome
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet(Name = "SearchAutor")]
    [ProducesResponseType(typeof(ApiBaseResponse<SearchAutorResult>), StatusCodes.Status200OK)]
    public async Task<ActionResult<SearchAutorResult>> SearchAutor(
        [FromQuery] SearchAutorRequest request,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _searchAutorUsecase.Handle(request, cancellationToken));
    }
}
