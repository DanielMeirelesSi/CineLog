using CatalogoApi.Domain.Entities;
using CatalogoApi.Infrastructure.Interfaces;

namespace CatalogoApi.Infrastructure.Repositories;

public class FilmeRepository : IRepository<Filme>
{
    private readonly Dictionary<Guid, Filme> _store = [];

    public Filme? GetById(Guid id) => _store.GetValueOrDefault(id);
    public IReadOnlyList<Filme> GetAll() => [.. _store.Values];
    public void Add(Filme entity) => _store[entity.Id] = entity;
    public void Update(Filme entity) => _store[entity.Id] = entity;
    public void Remove(Guid id) => _store.Remove(id);
    public bool Exists(Guid id) => _store.ContainsKey(id);
}
