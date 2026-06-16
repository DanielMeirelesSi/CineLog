using CatalogoApi.Api.Dtos.Requests;
using CatalogoApi.Api.Dtos.Responses;
using CatalogoApi.Application.Interfaces;
using CatalogoApi.Domain.Entities;
using CatalogoApi.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApi.Api.Controllers;

[ApiController]
[Route("filmes")]
[Produces("application/json")]
[Consumes("application/json")]
public class FilmesController(IFilmeService filmeService) : ControllerBase
{
    /// <summary>Lista todos os filmes cadastrados.</summary>
    /// <param name="genero">Filtra por gênero (opcional).</param>
    /// <param name="classificacao">Filtra por classificação etária (opcional).</param>
    /// <response code="200">Lista de filmes retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FilmeResponse>), StatusCodes.Status200OK)]
    public IActionResult Listar([FromQuery] Genero? genero, [FromQuery] ClassificacaoEtaria? classificacao)
        => Ok(filmeService.ListarTodos(genero, classificacao).Select(FilmeResponse.De));

    /// <summary>Retorna um filme pelo ID.</summary>
    /// <param name="id">ID do filme.</param>
    /// <response code="200">Filme encontrado.</response>
    /// <response code="404">Filme não encontrado.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(FilmeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    public IActionResult ObterPorId(Guid id) => Ok(FilmeResponse.De(filmeService.ObterPorId(id)));

    /// <summary>Cadastra um novo filme.</summary>
    /// <param name="request">Dados do filme a ser criado.</param>
    /// <response code="201">Filme criado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(FilmeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status400BadRequest)]
    public IActionResult Criar([FromBody] CriarFilmeRequest request)
    {
        Filme filme = filmeService.Criar(request.Titulo, request.Genero, request.AnoLancamento,
            request.Sinopse, request.Avaliacao, request.DuracaoMinutos, request.Diretor, request.Classificacao);
        return CreatedAtAction(nameof(ObterPorId), new { id = filme.Id }, FilmeResponse.De(filme));
    }

    /// <summary>Atualiza um filme existente.</summary>
    /// <param name="id">ID do filme.</param>
    /// <param name="request">Novos dados do filme.</param>
    /// <response code="200">Filme atualizado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="404">Filme não encontrado.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(FilmeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    public IActionResult Atualizar(Guid id, [FromBody] AtualizarFilmeRequest request)
    {
        Filme filme = filmeService.Atualizar(id, request.Titulo, request.Genero, request.AnoLancamento,
            request.Sinopse, request.Avaliacao, request.DuracaoMinutos, request.Diretor, request.Classificacao);
        return Ok(FilmeResponse.De(filme));
    }

    /// <summary>Remove um filme do catálogo.</summary>
    /// <param name="id">ID do filme.</param>
    /// <response code="204">Filme removido com sucesso.</response>
    /// <response code="404">Filme não encontrado.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    public IActionResult Deletar(Guid id) { filmeService.Deletar(id); return NoContent(); }
}
