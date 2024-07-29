using Basis.Biblioteca.API.Presentation;
using Basis.Biblioteca.Application.UseCases.Livro.Create;
using Basis.Biblioteca.Application.UseCases.Livro.Delete;
using Basis.Biblioteca.Application.UseCases.Livro.Get;
using Basis.Biblioteca.Application.UseCases.Livro.Search;
using Basis.Biblioteca.Application.UseCases.Livro.Update;
using Microsoft.AspNetCore.Mvc;

namespace Basis.Biblioteca.API.Controllers.V1;

[ApiController]
[Route("[controller]")]
public class LivrosController(
    ICreateLivroUsecase createLivroUsecase,
    ISearchLivroUsecase searchLivroUsecase,
    IGetLivroUsecase getLivroUsecase,
    IUpdateLivroUsecase updateLivroUsecase,
    IDeleteLivroUsecase deleteLivroUsecase,
    ILogger<LivrosController> logger) : BaseController
{
    private readonly ICreateLivroUsecase _createLivroUsecase = createLivroUsecase;
    private readonly ISearchLivroUsecase _searchLivroUsecase = searchLivroUsecase;
    private readonly IGetLivroUsecase _getLivroUsecase = getLivroUsecase;
    private readonly IUpdateLivroUsecase _updateLivroUsecase = updateLivroUsecase;
    private readonly IDeleteLivroUsecase _deleteLivroUsecase = deleteLivroUsecase;
    private readonly ILogger<LivrosController> _logger = logger;

    /// <summary>
    /// Cria um livro com o(s) preço(s) de venda, autor e assunto
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost(Name = "CreateLivro")]
    [ProducesResponseType(typeof(ApiBaseResponse<CreateLivroResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiBaseResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<CreateLivroResult>> CreateAsync(
        [FromBody] CreateLivroRequest request,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _createLivroUsecase.Handle(request, cancellationToken));
    }

    /// <summary>
    /// Pesquisa livros por titulo, assunto, editora, ano de publicação, autor ou assunto
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet(Name = "SearchLivro")]
    [Route("search")]
    [ProducesResponseType(typeof(ApiBaseResponse<SearchLivroResult>), StatusCodes.Status200OK)]
    public async Task<ActionResult<SearchLivroResult>> SearchAsync(
        [FromQuery] SearchLivroRequest request,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _searchLivroUsecase.Handle(request, cancellationToken));
    }

    /// <summary>
    /// Obtem os dados de um livro
    /// </summary>
    /// <param name="codl">Código do livro</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{codl:int}", Name = "GetLivro")]
    [ProducesResponseType(typeof(ApiBaseResponse<GetLivroResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetLivroResult>> GetAsync(
        [FromRoute] int codl,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _getLivroUsecase.Handle(codl, cancellationToken));
    }

    /// <summary>
    /// Atualiza os dados do livro
    /// </summary>
    /// <param name="codl">Código do Livro</param>
    /// <param name="updateRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{codl:int}", Name = "UpdateLivro")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiBaseResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateLivroAsync(
        [FromRoute] int codl,
        [FromBody] UpdateLivroRequest updateRequest,
        CancellationToken cancellationToken = default)
    {
        var request = updateRequest with { CodL = codl };
        return NoContent(await _updateLivroUsecase.Handle(request, cancellationToken));
    }

    /// <summary>
    /// Exclui um livro
    /// </summary>
    /// <param name="codl">Código do Livro</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{codl:int}", Name = "DeleteLivro")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiBaseResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<SearchLivroResult>> SearchAutorAsync(
        [FromRoute] int codl,
        CancellationToken cancellationToken = default)
    {
        return NoContent(await _deleteLivroUsecase.Handle(codl, cancellationToken));
    }
}
