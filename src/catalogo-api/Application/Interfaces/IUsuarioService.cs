using CatalogoApi.Domain.Entities;

namespace CatalogoApi.Application.Interfaces;

public interface IUsuarioService
{
    IReadOnlyList<Usuario> ListarTodos();
    Usuario ObterPorId(Guid id);
    Usuario Criar(string nome, string email);
    Usuario Atualizar(Guid id, string nome, string email);
    void Deletar(Guid id);
    void AdicionarFavorito(Guid usuarioId, Guid obraId);
    void RemoverFavorito(Guid usuarioId, Guid obraId);
    IReadOnlyList<ObraAudiovisual> ObterFavoritos(Guid usuarioId);
}
