using CatalogoApi.Domain.Enums;

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
        NumeroTemporadas = numeroTemporadas;
        NumeroEpisodiosPorTemporada = numeroEpisodiosPorTemporada;
        Criador = criador;
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
        AtualizarDadosBase(titulo, genero, anoLancamento, sinopse, avaliacao, updatedAt);
        NumeroTemporadas = numeroTemporadas;
        NumeroEpisodiosPorTemporada = numeroEpisodiosPorTemporada;
        Criador = criador;
        Status = status;
    }
}
