using CatalogoApi.Domain.Entities;
using CatalogoApi.Domain.Enums;

namespace CatalogoApi.Application.Interfaces;

public interface ISerieService
{
    IReadOnlyList<Serie> ListarTodos(Genero? genero = null, StatusSerie? status = null);
    Serie ObterPorId(Guid id);
    Serie Criar(string titulo, Genero genero, int anoLancamento, string? sinopse, decimal avaliacao, int numeroTemporadas, int numeroEpisodiosPorTemporada, string criador, StatusSerie status);
    Serie Atualizar(Guid id, string titulo, Genero genero, int anoLancamento, string? sinopse, decimal avaliacao, int numeroTemporadas, int numeroEpisodiosPorTemporada, string criador, StatusSerie status);
    void Deletar(Guid id);
}
