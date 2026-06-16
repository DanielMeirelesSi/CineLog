using System.ComponentModel.DataAnnotations;
using CatalogoApi.Domain.Enums;

namespace CatalogoApi.Api.Dtos.Requests;

public record CriarFilmeRequest(
    [Required][MaxLength(200)] string Titulo,
    [Required] Genero Genero,
    [Range(1888, 2100)] int AnoLancamento,
    [MaxLength(2000)] string? Sinopse,
    [Range(0.0, 10.0)] decimal Avaliacao,
    [Range(1, int.MaxValue)] int DuracaoMinutos,
    [Required][MaxLength(150)] string Diretor,
    [Required] ClassificacaoEtaria Classificacao
);
