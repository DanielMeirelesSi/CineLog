using System.Text.Json.Serialization;
using CatalogoApi.Domain.Exceptions;

namespace CatalogoApi.Domain.Entities;

public class Avaliacao
{
    public Guid Id { get; private set; }
    public Guid UsuarioId { get; private set; }
    public Guid ObraId { get; private set; }
    public decimal Nota { get; private set; }
    public string? Comentario { get; private set; }
    public DateTime CreatedAt { get; private set; }

    [JsonConstructor]
    public Avaliacao(
        Guid id,
        Guid usuarioId,
        Guid obraId,
        decimal nota,
        string? comentario,
        DateTime createdAt)
    {
        Validar(usuarioId, obraId, nota, comentario);

        Id = id;
        UsuarioId = usuarioId;
        ObraId = obraId;
        Nota = nota;
        Comentario = string.IsNullOrWhiteSpace(comentario) ? null : comentario.Trim();
        CreatedAt = createdAt;
    }

    public static Avaliacao Criar(Guid usuarioId, Guid obraId, decimal nota, string? comentario)
    {
        return new Avaliacao(
            Guid.NewGuid(),
            usuarioId,
            obraId,
            nota,
            comentario,
            DateTime.UtcNow);
    }

    private static void Validar(Guid usuarioId, Guid obraId, decimal nota, string? comentario)
    {
        if (usuarioId == Guid.Empty)
            throw new DomainException("O usuário da avaliação é obrigatório.");

        if (obraId == Guid.Empty)
            throw new DomainException("A obra avaliada é obrigatória.");

        if (nota < 0 || nota > 10)
            throw new DomainException("A nota da avaliação deve estar entre 0 e 10.");

        if (!string.IsNullOrWhiteSpace(comentario) && comentario.Trim().Length > 1000)
            throw new DomainException("O comentário deve ter no máximo 1000 caracteres.");
    }
}