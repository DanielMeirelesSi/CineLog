using CatalogoApi.Api.Dtos.Responses;
using CatalogoApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApi.Api.Controllers;

[ApiController]
[Route("usuarios/{usuarioId:guid}/favoritos")]
[Route("api/usuarios/{usuarioId:guid}/favoritos")]
[Produces("application/json")]
public class FavoritosController(IUsuarioService usuarioService) : ControllerBase
{
    /// <summary>Lista as obras favoritas de um usuário.</summary>
    /// <param name="usuarioId">ID do usuário.</param>
    /// <response code="200">Lista de favoritos retornada com sucesso.</response>
    /// <response code="404">Usuário não encontrado.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ObraResumoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    public IActionResult Listar(Guid usuarioId)
        => Ok(usuarioService.ObterFavoritos(usuarioId).Select(ObraResumoResponse.De));

    /// <summary>Adiciona uma obra aos favoritos do usuário.</summary>
    /// <param name="usuarioId">ID do usuário.</param>
    /// <param name="obraId">ID da obra.</param>
    /// <response code="204">Obra adicionada aos favoritos.</response>
    /// <response code="404">Usuário ou obra não encontrada.</response>
    /// <response code="409">Obra já está nos favoritos.</response>
    [HttpPost("{obraId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status409Conflict)]
    public IActionResult Adicionar(Guid usuarioId, Guid obraId)
    {
        usuarioService.AdicionarFavorito(usuarioId, obraId);
        return NoContent();
    }

    /// <summary>Remove uma obra dos favoritos do usuário.</summary>
    /// <param name="usuarioId">ID do usuário.</param>
    /// <param name="obraId">ID da obra.</param>
    /// <response code="204">Obra removida dos favoritos.</response>
    /// <response code="404">Usuário não encontrado ou obra não está nos favoritos.</response>
    [HttpDelete("{obraId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    public IActionResult Remover(Guid usuarioId, Guid obraId)
    {
        usuarioService.RemoverFavorito(usuarioId, obraId);
        return NoContent();
    }
}
