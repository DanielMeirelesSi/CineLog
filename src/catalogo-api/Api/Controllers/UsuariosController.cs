using CatalogoApi.Api.Dtos.Requests;
using CatalogoApi.Api.Dtos.Responses;
using CatalogoApi.Application.Interfaces;
using CatalogoApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApi.Api.Controllers;

[ApiController]
[Route("usuarios")]
[Produces("application/json")]
[Consumes("application/json")]
public class UsuariosController(IUsuarioService usuarioService) : ControllerBase
{
    /// <summary>Lista todos os usuários cadastrados.</summary>
    /// <response code="200">Lista de usuários retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UsuarioResponse>), StatusCodes.Status200OK)]
    public IActionResult Listar() => Ok(usuarioService.ListarTodos().Select(UsuarioResponse.De));

    /// <summary>Retorna um usuário pelo ID.</summary>
    /// <param name="id">ID do usuário.</param>
    /// <response code="200">Usuário encontrado.</response>
    /// <response code="404">Usuário não encontrado.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    public IActionResult ObterPorId(Guid id) => Ok(UsuarioResponse.De(usuarioService.ObterPorId(id)));

    /// <summary>Cadastra um novo usuário.</summary>
    /// <param name="request">Dados do usuário a ser criado.</param>
    /// <response code="201">Usuário criado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="409">E-mail já está em uso.</response>
    [HttpPost]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status409Conflict)]
    public IActionResult Criar([FromBody] CriarUsuarioRequest request)
    {
        Usuario usuario = usuarioService.Criar(request.Nome, request.Email);
        return CreatedAtAction(nameof(ObterPorId), new { id = usuario.Id }, UsuarioResponse.De(usuario));
    }

    /// <summary>Atualiza os dados de um usuário.</summary>
    /// <param name="id">ID do usuário.</param>
    /// <param name="request">Novos dados do usuário.</param>
    /// <response code="200">Usuário atualizado com sucesso.</response>
    /// <response code="404">Usuário não encontrado.</response>
    /// <response code="409">E-mail já está em uso por outro usuário.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status409Conflict)]
    public IActionResult Atualizar(Guid id, [FromBody] AtualizarUsuarioRequest request)
        => Ok(UsuarioResponse.De(usuarioService.Atualizar(id, request.Nome, request.Email)));

    /// <summary>Remove um usuário.</summary>
    /// <param name="id">ID do usuário.</param>
    /// <response code="204">Usuário removido com sucesso.</response>
    /// <response code="404">Usuário não encontrado.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    public IActionResult Deletar(Guid id) { usuarioService.Deletar(id); return NoContent(); }
}
