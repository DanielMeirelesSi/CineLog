using CatalogoApi.Domain.Exceptions;

namespace CatalogoApi.Domain.Entities;

public class Usuario
{
    private readonly List<Guid> _favoritos = [];

    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public IReadOnlyList<Guid> Favoritos => _favoritos.AsReadOnly();
    public DateTime CreatedAt { get; private set; }

    public Usuario(Guid id, string nome, string email, DateTime createdAt)
    {
        Id = id;
        Nome = nome;
        Email = email;
        CreatedAt = createdAt;
    }

    public void AdicionarFavorito(Guid obraId)
    {
        if (_favoritos.Contains(obraId))
            throw new ConflictException("A obra já está nos favoritos.");
        _favoritos.Add(obraId);
    }

    public void RemoverFavorito(Guid obraId)
    {
        if (!_favoritos.Contains(obraId))
            throw new NotFoundException("A obra não está nos favoritos do usuário.");
        _favoritos.Remove(obraId);
    }

    public void RemoverFavoritoSeExistir(Guid obraId) => _favoritos.Remove(obraId);

    public bool PossuiFavorito(Guid obraId) => _favoritos.Contains(obraId);

    public void Atualizar(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
}
