using CatalogoApi.Domain.Entities;

namespace CatalogoApi.Application.Interfaces;

public interface IAvaliacaoService
{
    IReadOnlyList<Avaliacao> ListarPorObra(Guid obraId);
    IReadOnlyList<Avaliacao> ListarPorUsuario(Guid usuarioId);
    Avaliacao Avaliar(Guid usuarioId, Guid obraId, decimal nota, string? comentario);
}