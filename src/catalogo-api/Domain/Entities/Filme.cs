using CatalogoApi.Domain.Enums;
using CatalogoApi.Domain.Exceptions;

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
        ValidarDadosFilme(duracaoMinutos, diretor);

        DuracaoMinutos = duracaoMinutos;
        Diretor = diretor.Trim();
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
        ValidarDadosFilme(duracaoMinutos, diretor);

        AtualizarDadosBase(titulo, genero, anoLancamento, sinopse, avaliacao, updatedAt);
        DuracaoMinutos = duracaoMinutos;
        Diretor = diretor.Trim();
        Classificacao = classificacao;
    }

    private static void ValidarDadosFilme(int duracaoMinutos, string diretor)
    {
        if (duracaoMinutos <= 0)
            throw new DomainException("A duração do filme deve ser maior que zero.");

        if (string.IsNullOrWhiteSpace(diretor))
            throw new DomainException("O diretor do filme é obrigatório.");

        if (diretor.Trim().Length > 150)
            throw new DomainException("O nome do diretor deve ter no máximo 150 caracteres.");
    }
}