using CatalogoApi.Domain.Entities;
using CatalogoApi.Infrastructure.Interfaces;

namespace CatalogoApi.Infrastructure.Repositories;

public class SerieRepository : IRepository<Serie>
{
    private readonly Dictionary<Guid, Serie> _store = [];

    public Serie? GetById(Guid id) => _store.GetValueOrDefault(id);
    public IReadOnlyList<Serie> GetAll() => [.. _store.Values];
    public void Add(Serie entity) => _store[entity.Id] = entity;
    public void Update(Serie entity) => _store[entity.Id] = entity;
    public void Remove(Guid id) => _store.Remove(id);
    public bool Exists(Guid id) => _store.ContainsKey(id);
}
