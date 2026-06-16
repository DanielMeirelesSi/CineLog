using CatalogoApi.Domain.Enums;

namespace CatalogoApi.Domain.Entities;

public abstract class ObraAudiovisual
{
    public Guid Id { get; private set; }
    public string Titulo { get; private set; }
    public Genero Genero { get; private set; }
    public int AnoLancamento { get; private set; }
    public string? Sinopse { get; private set; }
    public decimal Avaliacao { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    protected ObraAudiovisual(
        Guid id,
        string titulo,
        Genero genero,
        int anoLancamento,
        string? sinopse,
        decimal avaliacao,
        DateTime createdAt)
    {
        Id = id;
        Titulo = titulo;
        Genero = genero;
        AnoLancamento = anoLancamento;
        Sinopse = sinopse;
        Avaliacao = avaliacao;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
    }

    public abstract string ObterDetalhes();

    protected void AtualizarDadosBase(
        string titulo,
        Genero genero,
        int anoLancamento,
        string? sinopse,
        decimal avaliacao,
        DateTime updatedAt)
    {
        Titulo = titulo;
        Genero = genero;
        AnoLancamento = anoLancamento;
        Sinopse = sinopse;
        Avaliacao = avaliacao;
        UpdatedAt = updatedAt;
    }
}
