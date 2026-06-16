using CatalogoApi.Application.Interfaces;
using CatalogoApi.Domain.Entities;
using CatalogoApi.Domain.Exceptions;
using CatalogoApi.Infrastructure.Interfaces;

namespace CatalogoApi.Application.Services;

public class UsuarioService(IUsuarioRepository usuarioRepository, ICatalogoService catalogoService) : IUsuarioService
{
    public IReadOnlyList<Usuario> ListarTodos() => usuarioRepository.GetAll();

    public Usuario ObterPorId(Guid id) =>
        usuarioRepository.GetById(id) ?? throw new NotFoundException($"Usuário com id '{id}' não encontrado.");

    public Usuario Criar(string nome, string email)
    {
        if (usuarioRepository.EmailJaCadastrado(email))
            throw new ConflictException($"O e-mail '{email}' já está em uso.");

        Usuario usuario = new(Guid.NewGuid(), nome, email, DateTime.UtcNow);
        usuarioRepository.Add(usuario);
        return usuario;
    }

    public Usuario Atualizar(Guid id, string nome, string email)
    {
        Usuario usuario = ObterPorId(id);

        if (usuarioRepository.EmailJaCadastrado(email, id))
            throw new ConflictException($"O e-mail '{email}' já está em uso por outro usuário.");

        usuario.Atualizar(nome, email);
        usuarioRepository.Update(usuario);
        return usuario;
    }

    public void Deletar(Guid id)
    {
        if (!usuarioRepository.Exists(id))
            throw new NotFoundException($"Usuário com id '{id}' não encontrado.");
        usuarioRepository.Remove(id);
    }

    public void AdicionarFavorito(Guid usuarioId, Guid obraId)
    {
        Usuario usuario = ObterPorId(usuarioId);
        catalogoService.ObterPorId(obraId);
        usuario.AdicionarFavorito(obraId);
        usuarioRepository.Update(usuario);
    }

    public void RemoverFavorito(Guid usuarioId, Guid obraId)
    {
        Usuario usuario = ObterPorId(usuarioId);
        usuario.RemoverFavorito(obraId);
        usuarioRepository.Update(usuario);
    }

    public IReadOnlyList<ObraAudiovisual> ObterFavoritos(Guid usuarioId)
    {
        Usuario usuario = ObterPorId(usuarioId);
        return usuario.Favoritos
            .Select(obraId => catalogoService.ObterPorId(obraId))
            .ToList()
            .AsReadOnly();
    }
}
