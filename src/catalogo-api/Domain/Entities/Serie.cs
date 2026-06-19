using CatalogoApi.Domain.Enums;
using CatalogoApi.Domain.Exceptions;

namespace CatalogoApi.Domain.Entities;

public class Serie : ObraAudiovisual
{
    public int NumeroTemporadas { get; private set; }
    public int NumeroEpisodiosPorTemporada { get; private set; }
    public string Criador { get; private set; }
    public StatusSerie Status { get; private set; }

    public Serie(
        Guid id,
        string titulo,
        Genero genero,
        int anoLancamento,
        string? sinopse,
        decimal avaliacao,
        int numeroTemporadas,
        int numeroEpisodiosPorTemporada,
        string criador,
        StatusSerie status,
        DateTime createdAt)
        : base(id, titulo, genero, anoLancamento, sinopse, avaliacao, createdAt)
    {
        ValidarDadosSerie(numeroTemporadas, numeroEpisodiosPorTemporada, criador);

        NumeroTemporadas = numeroTemporadas;
        NumeroEpisodiosPorTemporada = numeroEpisodiosPorTemporada;
        Criador = criador.Trim();
        Status = status;
    }

    public override string ObterDetalhes() =>
        $"Série: {Titulo} | Criador: {Criador} | Temporadas: {NumeroTemporadas} | Status: {Status}";

    public void Atualizar(
        string titulo,
        Genero genero,
        int anoLancamento,
        string? sinopse,
        decimal avaliacao,
        int numeroTemporadas,
        int numeroEpisodiosPorTemporada,
        string criador,
        StatusSerie status,
        DateTime updatedAt)
    {
        ValidarDadosSerie(numeroTemporadas, numeroEpisodiosPorTemporada, criador);

        AtualizarDadosBase(titulo, genero, anoLancamento, sinopse, avaliacao, updatedAt);
        NumeroTemporadas = numeroTemporadas;
        NumeroEpisodiosPorTemporada = numeroEpisodiosPorTemporada;
        Criador = criador.Trim();
        Status = status;
    }

    private static void ValidarDadosSerie(
        int numeroTemporadas,
        int numeroEpisodiosPorTemporada,
        string criador)
    {
        if (numeroTemporadas <= 0)
            throw new DomainException("O número de temporadas deve ser maior que zero.");

        if (numeroEpisodiosPorTemporada <= 0)
            throw new DomainException("O número de episódios por temporada deve ser maior que zero.");

        if (string.IsNullOrWhiteSpace(criador))
            throw new DomainException("O criador da série é obrigatório.");

        if (criador.Trim().Length > 150)
            throw new DomainException("O nome do criador deve ter no máximo 150 caracteres.");
    }
}