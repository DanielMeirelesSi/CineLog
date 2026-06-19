using System.ComponentModel.DataAnnotations;

namespace CatalogoApi.Api.Dtos.Requests;

public class CriarAvaliacaoRequest
{
    [Range(0, 10, ErrorMessage = "A nota deve estar entre 0 e 10.")]
    public decimal Nota { get; set; }

    [MaxLength(1000, ErrorMessage = "O comentário deve ter no máximo 1000 caracteres.")]
    public string? Comentario { get; set; }
}