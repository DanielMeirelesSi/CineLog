using CatalogoApi.Application.Interfaces;
using CatalogoApi.Domain.Entities;
using CatalogoApi.Domain.Enums;
using CatalogoApi.Domain.Exceptions;
using CatalogoApi.Infrastructure.Interfaces;

namespace CatalogoApi.Application.Services;

public class SerieService(IRepository<Serie> serieRepository, IRepository<Usuario> usuarioRepository) : ISerieService
{
    public IReadOnlyList<Serie> ListarTodos(Genero? genero = null, StatusSerie? status = null)
    {
        IEnumerable<Serie> series = serieRepository.GetAll();
        if (genero.HasValue) series = series.Where(s => s.Genero == genero.Value);
        if (status.HasValue) series = series.Where(s => s.Status == status.Value);
        return series.ToList().AsReadOnly();
    }

    public Serie ObterPorId(Guid id) =>
        serieRepository.GetById(id) ?? throw new NotFoundException($"Série com id '{id}' não encontrada.");

    public Serie Criar(string titulo, Genero genero, int anoLancamento, string? sinopse, decimal avaliacao, int numeroTemporadas, int numeroEpisodiosPorTemporada, string criador, StatusSerie status)
    {
        Serie serie = new(Guid.NewGuid(), titulo, genero, anoLancamento, sinopse, avaliacao, numeroTemporadas, numeroEpisodiosPorTemporada, criador, status, DateTime.UtcNow);
        serieRepository.Add(serie);
        return serie;
    }

    public Serie Atualizar(Guid id, string titulo, Genero genero, int anoLancamento, string? sinopse, decimal avaliacao, int numeroTemporadas, int numeroEpisodiosPorTemporada, string criador, StatusSerie status)
    {
        Serie serie = ObterPorId(id);
        serie.Atualizar(titulo, genero, anoLancamento, sinopse, avaliacao, numeroTemporadas, numeroEpisodiosPorTemporada, criador, status, DateTime.UtcNow);
        serieRepository.Update(serie);
        return serie;
    }

    public void Deletar(Guid id)
    {
        if (!serieRepository.Exists(id))
            throw new NotFoundException($"Série com id '{id}' não encontrada.");

        serieRepository.Remove(id);

        foreach (Usuario usuario in usuarioRepository.GetAll())
        {
            usuario.RemoverFavoritoSeExistir(id);
            usuarioRepository.Update(usuario);
        }
    }
}
