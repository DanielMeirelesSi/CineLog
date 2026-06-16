using CatalogoApi.Domain.Entities;
using CatalogoApi.Domain.Enums;

namespace CatalogoApi.Application.Interfaces;

public interface ICatalogoService
{
    IReadOnlyList<ObraAudiovisual> ListarTodos(string? tipo = null, Genero? genero = null);
    IReadOnlyList<ObraAudiovisual> BuscarPorTitulo(string titulo);
    IReadOnlyList<ObraAudiovisual> FiltrarPorGenero(Genero genero);
    ObraAudiovisual ObterPorId(Guid id);
}
