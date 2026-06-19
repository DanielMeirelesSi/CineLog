using CatalogoApi.Domain.Enums;
using CatalogoApi.Domain.Exceptions;

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
        ValidarDadosBase(titulo, anoLancamento, sinopse, avaliacao);

        Id = id;
        Titulo = titulo.Trim();
        Genero = genero;
        AnoLancamento = anoLancamento;
        Sinopse = string.IsNullOrWhiteSpace(sinopse) ? null : sinopse.Trim();
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
        ValidarDadosBase(titulo, anoLancamento, sinopse, avaliacao);

        Titulo = titulo.Trim();
        Genero = genero;
        AnoLancamento = anoLancamento;
        Sinopse = string.IsNullOrWhiteSpace(sinopse) ? null : sinopse.Trim();
        Avaliacao = avaliacao;
        UpdatedAt = updatedAt;
    }

    private static void ValidarDadosBase(
        string titulo,
        int anoLancamento,
        string? sinopse,
        decimal avaliacao)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new DomainException("O título da obra é obrigatório.");

        if (titulo.Trim().Length > 200)
            throw new DomainException("O título da obra deve ter no máximo 200 caracteres.");

        if (anoLancamento < 1888 || anoLancamento > 2100)
            throw new DomainException("O ano de lançamento deve estar entre 1888 e 2100.");

        if (!string.IsNullOrWhiteSpace(sinopse) && sinopse.Trim().Length > 2000)
            throw new DomainException("A sinopse deve ter no máximo 2000 caracteres.");

        if (avaliacao < 0 || avaliacao > 10)
            throw new DomainException("A avaliação deve estar entre 0 e 10.");
    }
}