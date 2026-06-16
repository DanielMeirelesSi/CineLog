using CatalogoApi.Api.Dtos.Responses;
using CatalogoApi.Application.Interfaces;
using CatalogoApi.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApi.Api.Controllers;

[ApiController]
[Route("catalogo")]
[Produces("application/json")]
public class CatalogoController(ICatalogoService catalogoService) : ControllerBase
{
    /// <summary>Lista todas as obras do catálogo com filtros opcionais.</summary>
    /// <param name="tipo">Tipo de obra: "filme" ou "serie" (opcional).</param>
    /// <param name="genero">Filtra por gênero (opcional).</param>
    /// <response code="200">Lista de obras retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ObraResumoResponse>), StatusCodes.Status200OK)]
    public IActionResult Listar([FromQuery] string? tipo, [FromQuery] Genero? genero)
        => Ok(catalogoService.ListarTodos(tipo, genero).Select(ObraResumoResponse.De));

    /// <summary>Pesquisa obras pelo título (busca parcial, case-insensitive).</summary>
    /// <param name="titulo">Texto a pesquisar no título.</param>
    /// <response code="200">Resultados da busca.</response>
    /// <response code="400">Parâmetro título não informado.</response>
    [HttpGet("buscar")]
    [ProducesResponseType(typeof(IEnumerable<ObraResumoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status400BadRequest)]
    public IActionResult Buscar([FromQuery] string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            return BadRequest(new ErroResponse("O parâmetro 'titulo' é obrigatório.", []));
        return Ok(catalogoService.BuscarPorTitulo(titulo).Select(ObraResumoResponse.De));
    }

    /// <summary>Lista todas as obras de um gênero específico.</summary>
    /// <param name="genero">Gênero a filtrar.</param>
    /// <response code="200">Obras do gênero informado.</response>
    [HttpGet("genero/{genero}")]
    [ProducesResponseType(typeof(IEnumerable<ObraResumoResponse>), StatusCodes.Status200OK)]
    public IActionResult FiltrarPorGenero(Genero genero)
        => Ok(catalogoService.FiltrarPorGenero(genero).Select(ObraResumoResponse.De));
}
