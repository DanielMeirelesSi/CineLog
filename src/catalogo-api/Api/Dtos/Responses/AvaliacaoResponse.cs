namespace CatalogoApi.Api.Dtos.Responses;

public record AvaliacaoResponse(
    Guid Id,
    Guid UsuarioId,
    string NomeUsuario,
    Guid ObraId,
    string TituloObra,
    decimal Nota,
    string? Comentario,
    DateTime CreatedAt
);