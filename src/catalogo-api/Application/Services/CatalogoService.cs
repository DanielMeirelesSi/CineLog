using CatalogoApi.Application.Interfaces;
using CatalogoApi.Domain.Entities;
using CatalogoApi.Domain.Enums;
using CatalogoApi.Domain.Exceptions;
using CatalogoApi.Infrastructure.Interfaces;

namespace CatalogoApi.Application.Services;

public class CatalogoService(IRepository<Filme> filmeRepository, IRepository<Serie> serieRepository) : ICatalogoService
{
    public IReadOnlyList<ObraAudiovisual> ListarTodos(string? tipo = null, Genero? genero = null)
    {
        Catalogo catalogo = CriarCatalogo();

        IReadOnlyList<ObraAudiovisual> obras = tipo?.ToLowerInvariant() switch
        {
            "filme" => catalogo.FiltrarFilmes(),
            "serie" => catalogo.FiltrarSeries(),
            _ => catalogo.ListarTodos()
        };

        if (genero.HasValue)
            return obras.Where(o => o.Genero == genero.Value).ToList().AsReadOnly();

        return obras;
    }

    public IReadOnlyList<ObraAudiovisual> BuscarPorTitulo(string titulo) =>
        CriarCatalogo().BuscarPorTitulo(titulo);

    public IReadOnlyList<ObraAudiovisual> FiltrarPorGenero(Genero genero) =>
        CriarCatalogo().FiltrarPorGenero(genero);

    public ObraAudiovisual ObterPorId(Guid id) =>
        (ObraAudiovisual?)filmeRepository.GetById(id)
        ?? (ObraAudiovisual?)serieRepository.GetById(id)
        ?? throw new NotFoundException($"Obra com id '{id}' não encontrada.");

    private Catalogo CriarCatalogo()
    {
        Catalogo catalogo = new();
        foreach (Filme filme in filmeRepository.GetAll()) catalogo.Adicionar(filme);
        foreach (Serie serie in serieRepository.GetAll()) catalogo.Adicionar(serie);
        return catalogo;
    }
}
