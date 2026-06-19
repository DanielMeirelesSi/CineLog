using System.Net.Mail;
using CatalogoApi.Domain.Exceptions;

namespace CatalogoApi.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public List<Guid> Favoritos { get; private set; } = [];
    public DateTime CreatedAt { get; private set; }

    public Usuario(
        Guid id,
        string nome,
        string email,
        DateTime createdAt,
        List<Guid>? favoritos = null)
    {
        ValidarDadosUsuario(nome, email);

        Id = id;
        Nome = nome.Trim();
        Email = email.Trim().ToLowerInvariant();
        CreatedAt = createdAt;
        Favoritos = favoritos?.Distinct().ToList() ?? [];
    }

    public void AdicionarFavorito(Guid obraId)
    {
        if (obraId == Guid.Empty)
            throw new DomainException("O identificador da obra é inválido.");

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
        ValidarDadosUsuario(nome, email);

        Nome = nome.Trim();
        Email = email.Trim().ToLowerInvariant();
    }

    private static void ValidarDadosUsuario(string nome, string email)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("O nome do usuário é obrigatório.");

        if (nome.Trim().Length > 100)
            throw new DomainException("O nome do usuário deve ter no máximo 100 caracteres.");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("O e-mail do usuário é obrigatório.");

        if (email.Trim().Length > 254)
            throw new DomainException("O e-mail do usuário deve ter no máximo 254 caracteres.");

        try
        {
            _ = new MailAddress(email.Trim());
        }
        catch
        {
            throw new DomainException("O e-mail informado é inválido.");
        }
    }
}