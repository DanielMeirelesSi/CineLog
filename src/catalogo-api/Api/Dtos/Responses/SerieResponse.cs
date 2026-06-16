using CatalogoApi.Domain.Entities;

namespace CatalogoApi.Api.Dtos.Responses;

public record SerieResponse(
    Guid Id,
    string Titulo,
    string Genero,
    int AnoLancamento,
    string? Sinopse,
    decimal Avaliacao,
    int NumeroTemporadas,
    int NumeroEpisodiosPorTemporada,
    string Criador,
    string Status,
    DateTime CreatedAt,
    DateTime UpdatedAt)
{
    public static SerieResponse De(Serie serie) => new(
        serie.Id,
        serie.Titulo,
        serie.Genero.ToString(),
        serie.AnoLancamento,
        serie.Sinopse,
        serie.Avaliacao,
        serie.NumeroTemporadas,
        serie.NumeroEpisodiosPorTemporada,
        serie.Criador,
        serie.Status.ToString(),
        serie.CreatedAt,
        serie.UpdatedAt);
}
