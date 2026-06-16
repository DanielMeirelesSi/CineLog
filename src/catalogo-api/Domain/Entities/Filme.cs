using CatalogoApi.Domain.Enums;

namespace CatalogoApi.Domain.Entities;

public class Filme : ObraAudiovisual
{
    public int DuracaoMinutos { get; private set; }
    public string Diretor { get; private set; }
    public ClassificacaoEtaria Classificacao { get; private set; }

    public Filme(
        Guid id,
        string titulo,
        Genero genero,
        int anoLancamento,
        string? sinopse,
        decimal avaliacao,
        int duracaoMinutos,
        string diretor,
        ClassificacaoEtaria classificacao,
        DateTime createdAt)
        : base(id, titulo, genero, anoLancamento, sinopse, avaliacao, createdAt)
    {
        DuracaoMinutos = duracaoMinutos;
        Diretor = diretor;
        Classificacao = classificacao;
    }

    public override string ObterDetalhes() =>
        $"Filme: {Titulo} | Diretor: {Diretor} | Duração: {DuracaoMinutos} min | Classificação: {Classificacao}";

    public void Atualizar(
        string titulo,
        Genero genero,
        int anoLancamento,
        string? sinopse,
        decimal avaliacao,
        int duracaoMinutos,
        string diretor,
        ClassificacaoEtaria classificacao,
        DateTime updatedAt)
    {
        AtualizarDadosBase(titulo, genero, anoLancamento, sinopse, avaliacao, updatedAt);
        DuracaoMinutos = duracaoMinutos;
        Diretor = diretor;
        Classificacao = classificacao;
    }
}
