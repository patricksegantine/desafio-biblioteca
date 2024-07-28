using Basis.Biblioteca.API.Presentation;
using Basis.Biblioteca.Application.UseCases.Assunto.Search;
using Microsoft.AspNetCore.Mvc;

namespace Basis.Biblioteca.API.Controllers.V1;

[ApiController]
[Route("[controller]")]
public class AssuntosController(
    ISearchAssuntoUsecase searchAssuntoUsecase,
    ILogger<AssuntosController> logger) : BaseController
{
    private readonly ISearchAssuntoUsecase _searchAssuntoUsecase = searchAssuntoUsecase;
    private readonly ILogger<AssuntosController> _logger = logger;

    /// <summary>
    /// Pesquisa assuntos pela descrição
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet(Name = "SearchAssunto")]
    [ProducesResponseType(typeof(ApiBaseResponse<SearchAssuntoResult>), StatusCodes.Status200OK)]
    public async Task<ActionResult<SearchAssuntoResult>> SearchAssunto(
        [FromQuery] SearchAssuntoRequest request,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _searchAssuntoUsecase.Handle(request, cancellationToken));
    }
}