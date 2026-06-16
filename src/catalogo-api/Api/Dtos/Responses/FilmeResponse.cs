using CatalogoApi.Domain.Entities;

namespace CatalogoApi.Api.Dtos.Responses;

public record FilmeResponse(
    Guid Id,
    string Titulo,
    string Genero,
    int AnoLancamento,
    string? Sinopse,
    decimal Avaliacao,
    int DuracaoMinutos,
    string Diretor,
    string Classificacao,
    DateTime CreatedAt,
    DateTime UpdatedAt)
{
    public static FilmeResponse De(Filme filme) => new(
        filme.Id,
        filme.Titulo,
        filme.Genero.ToString(),
        filme.AnoLancamento,
        filme.Sinopse,
        filme.Avaliacao,
        filme.DuracaoMinutos,
        filme.Diretor,
        filme.Classificacao.ToString(),
        filme.CreatedAt,
        filme.UpdatedAt);
}
