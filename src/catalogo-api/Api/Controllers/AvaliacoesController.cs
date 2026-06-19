using CatalogoApi.Api.Dtos.Requests;
using CatalogoApi.Api.Dtos.Responses;
using CatalogoApi.Application.Interfaces;
using CatalogoApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApi.Api.Controllers;

[ApiController]
[Route("avaliacoes")]
[Route("api/avaliacoes")]
[Produces("application/json")]
public class AvaliacoesController(
    IAvaliacaoService avaliacaoService,
    IUsuarioService usuarioService,
    ICatalogoService catalogoService) : ControllerBase
{
    [HttpGet("obra/{obraId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<AvaliacaoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    public IActionResult ListarPorObra(Guid obraId)
    {
        IEnumerable<AvaliacaoResponse> response = avaliacaoService
            .ListarPorObra(obraId)
            .Select(ParaResponse);

        return Ok(response);
    }

    [HttpGet("usuario/{usuarioId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<AvaliacaoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    public IActionResult ListarPorUsuario(Guid usuarioId)
    {
        IEnumerable<AvaliacaoResponse> response = avaliacaoService
            .ListarPorUsuario(usuarioId)
            .Select(ParaResponse);

        return Ok(response);
    }

    [HttpPost("usuarios/{usuarioId:guid}/obras/{obraId:guid}")]
    [ProducesResponseType(typeof(AvaliacaoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status422UnprocessableEntity)]
    public IActionResult Avaliar(Guid usuarioId, Guid obraId, CriarAvaliacaoRequest request)
    {
        Avaliacao avaliacao = avaliacaoService.Avaliar(
            usuarioId,
            obraId,
            request.Nota,
            request.Comentario);

        AvaliacaoResponse response = ParaResponse(avaliacao);

        return StatusCode(StatusCodes.Status201Created, response);
    }

    private AvaliacaoResponse ParaResponse(Avaliacao avaliacao)
    {
        Usuario usuario = usuarioService.ObterPorId(avaliacao.UsuarioId);
        ObraAudiovisual obra = catalogoService.ObterPorId(avaliacao.ObraId);

        return new AvaliacaoResponse(
            avaliacao.Id,
            avaliacao.UsuarioId,
            usuario.Nome,
            avaliacao.ObraId,
            obra.Titulo,
            avaliacao.Nota,
            avaliacao.Comentario,
            avaliacao.CreatedAt);
    }
}