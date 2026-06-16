using CatalogoApi.Domain.Entities;

namespace CatalogoApi.Api.Dtos.Responses;

public record ObraResumoResponse(
    Guid Id,
    string Titulo,
    string Genero,
    int AnoLancamento,
    decimal Avaliacao,
    string Tipo,
    string Detalhes)
{
    public static ObraResumoResponse De(ObraAudiovisual obra) => new(
        obra.Id,
        obra.Titulo,
        obra.Genero.ToString(),
        obra.AnoLancamento,
        obra.Avaliacao,
        obra is Filme ? "Filme" : "Serie",
        obra.ObterDetalhes());
}
