using CatalogoApi.Domain.Entities;
using CatalogoApi.Infrastructure.Interfaces;

namespace CatalogoApi.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly Dictionary<Guid, Usuario> _store = [];

    public Usuario? GetById(Guid id) => _store.GetValueOrDefault(id);
    public IReadOnlyList<Usuario> GetAll() => [.. _store.Values];
    public void Add(Usuario entity) => _store[entity.Id] = entity;
    public void Update(Usuario entity) => _store[entity.Id] = entity;
    public void Remove(Guid id) => _store.Remove(id);
    public bool Exists(Guid id) => _store.ContainsKey(id);
    public bool EmailJaCadastrado(string email, Guid? excluirId = null) =>
        _store.Values.Any(u => string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase) && u.Id != excluirId);
}
