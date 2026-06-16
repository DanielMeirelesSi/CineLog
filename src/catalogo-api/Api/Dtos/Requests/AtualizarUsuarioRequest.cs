using System.ComponentModel.DataAnnotations;

namespace CatalogoApi.Api.Dtos.Requests;

public record AtualizarUsuarioRequest(
    [Required][MaxLength(100)] string Nome,
    [Required][EmailAddress][MaxLength(254)] string Email
);
