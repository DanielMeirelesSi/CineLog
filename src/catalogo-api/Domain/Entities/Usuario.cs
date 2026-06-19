using CatalogoApi.Domain.Exceptions;

namespace CatalogoApi.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public List<Guid> Favoritos { get; private set; } = [];
    public DateTime CreatedAt { get; private set; }

    public Usuario(Guid id, string nome, string email, DateTime createdAt, List<Guid>? favoritos = null)
    {
        Id = id;
        Nome = nome;
        Email = email;
        CreatedAt = createdAt;
        Favoritos = favoritos?.Distinct().ToList() ?? [];
    }

    public void AdicionarFavorito(Guid obraId)
    {
        if (Favoritos.Contains(obraId))
            throw new ConflictException("A obra já está nos favoritos.");

        Favoritos.Add(obraId);
    }

    public void RemoverFavorito(Guid obraId)
    {
        if (!Favoritos.Contains(obraId))
            throw new NotFoundException("A obra não está nos favoritos do usuário.");

        Favoritos.Remove(obraId);
    }

    public void RemoverFavoritoSeExistir(Guid obraId)
    {
        Favoritos.Remove(obraId);
    }

    public bool PossuiFavorito(Guid obraId)
    {
        return Favoritos.Contains(obraId);
    }

    public void Atualizar(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
}