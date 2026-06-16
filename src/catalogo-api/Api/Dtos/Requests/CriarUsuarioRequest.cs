using System.ComponentModel.DataAnnotations;

namespace CatalogoApi.Api.Dtos.Requests;

public record CriarUsuarioRequest(
    [Required][MaxLength(100)] string Nome,
    [Required][EmailAddress][MaxLength(254)] string Email
);
