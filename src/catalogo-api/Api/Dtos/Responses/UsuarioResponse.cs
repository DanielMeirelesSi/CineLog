using CatalogoApi.Domain.Entities;

namespace CatalogoApi.Api.Dtos.Responses;

public record UsuarioResponse(
    Guid Id,
    string Nome,
    string Email,
    int TotalFavoritos,
    DateTime CreatedAt)
{
    public static UsuarioResponse De(Usuario usuario) => new(
        usuario.Id,
        usuario.Nome,
        usuario.Email,
        usuario.Favoritos.Count,
        usuario.CreatedAt);
}
