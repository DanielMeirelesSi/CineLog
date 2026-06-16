namespace CatalogoApi.Api.Dtos.Responses;

public record ErroResponse(string Erro, IEnumerable<string> Detalhes);
