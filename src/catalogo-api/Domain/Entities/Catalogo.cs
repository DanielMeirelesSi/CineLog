using CatalogoApi.Domain.Enums;
using CatalogoApi.Domain.Exceptions;

namespace CatalogoApi.Domain.Entities;

public class Catalogo
{
    private readonly List<ObraAudiovisual> _obras = [];

    public IReadOnlyList<ObraAudiovisual> Obras => _obras.AsReadOnly();

    public void Adicionar(ObraAudiovisual obra) => _obras.Add(obra);

    public void Remover(Guid id)
    {
        ObraAudiovisual obra = ObterPorId(id);
        _obras.Remove(obra);
    }

    public ObraAudiovisual ObterPorId(Guid id) =>
        _obras.FirstOrDefault(o => o.Id == id)
        ?? throw new NotFoundException($"Obra com id '{id}' não encontrada.");

    public IReadOnlyList<ObraAudiovisual> ListarTodos() => _obras.AsReadOnly();

    public IReadOnlyList<ObraAudiovisual> BuscarPorTitulo(string titulo) =>
        _obras.Where(o => o.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase))
              .ToList()
              .AsReadOnly();

    public IReadOnlyList<ObraAudiovisual> FiltrarPorGenero(Genero genero) =>
        _obras.Where(o => o.Genero == genero).ToList().AsReadOnly();

    public IReadOnlyList<ObraAudiovisual> FiltrarFilmes() =>
        _obras.OfType<Filme>().Cast<ObraAudiovisual>().ToList().AsReadOnly();

    public IReadOnlyList<ObraAudiovisual> FiltrarSeries() =>
        _obras.OfType<Serie>().Cast<ObraAudiovisual>().ToList().AsReadOnly();
}
