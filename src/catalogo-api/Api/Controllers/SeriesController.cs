using CatalogoApi.Api.Dtos.Requests;
using CatalogoApi.Api.Dtos.Responses;
using CatalogoApi.Application.Interfaces;
using CatalogoApi.Domain.Entities;
using CatalogoApi.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApi.Api.Controllers;

[ApiController]
[Route("series")]
[Produces("application/json")]
[Consumes("application/json")]
public class SeriesController(ISerieService serieService) : ControllerBase
{
    /// <summary>Lista todas as séries cadastradas.</summary>
    /// <param name="genero">Filtra por gênero (opcional).</param>
    /// <param name="status">Filtra por status da série (opcional).</param>
    /// <response code="200">Lista de séries retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SerieResponse>), StatusCodes.Status200OK)]
    public IActionResult Listar([FromQuery] Genero? genero, [FromQuery] StatusSerie? status)
        => Ok(serieService.ListarTodos(genero, status).Select(SerieResponse.De));

    /// <summary>Retorna uma série pelo ID.</summary>
    /// <param name="id">ID da série.</param>
    /// <response code="200">Série encontrada.</response>
    /// <response code="404">Série não encontrada.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SerieResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    public IActionResult ObterPorId(Guid id) => Ok(SerieResponse.De(serieService.ObterPorId(id)));

    /// <summary>Cadastra uma nova série.</summary>
    /// <param name="request">Dados da série a ser criada.</param>
    /// <response code="201">Série criada com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(SerieResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status400BadRequest)]
    public IActionResult Criar([FromBody] CriarSerieRequest request)
    {
        Serie serie = serieService.Criar(request.Titulo, request.Genero, request.AnoLancamento,
            request.Sinopse, request.Avaliacao, request.NumeroTemporadas,
            request.NumeroEpisodiosPorTemporada, request.Criador, request.Status);
        return CreatedAtAction(nameof(ObterPorId), new { id = serie.Id }, SerieResponse.De(serie));
    }

    /// <summary>Atualiza uma série existente.</summary>
    /// <param name="id">ID da série.</param>
    /// <param name="request">Novos dados da série.</param>
    /// <response code="200">Série atualizada com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="404">Série não encontrada.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(SerieResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    public IActionResult Atualizar(Guid id, [FromBody] AtualizarSerieRequest request)
    {
        Serie serie = serieService.Atualizar(id, request.Titulo, request.Genero, request.AnoLancamento,
            request.Sinopse, request.Avaliacao, request.NumeroTemporadas,
            request.NumeroEpisodiosPorTemporada, request.Criador, request.Status);
        return Ok(SerieResponse.De(serie));
    }

    /// <summary>Remove uma série do catálogo.</summary>
    /// <param name="id">ID da série.</param>
    /// <response code="204">Série removida com sucesso.</response>
    /// <response code="404">Série não encontrada.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    public IActionResult Deletar(Guid id) { serieService.Deletar(id); return NoContent(); }
}
