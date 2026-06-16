namespace CatalogoApi.Infrastructure.Interfaces;

public interface IRepository<T> where T : class
{
    T? GetById(Guid id);
    IReadOnlyList<T> GetAll();
    void Add(T entity);
    void Update(T entity);
    void Remove(Guid id);
    bool Exists(Guid id);
}
