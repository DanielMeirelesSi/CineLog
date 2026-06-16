using CatalogoApi.Domain.Entities;
using CatalogoApi.Domain.Enums;

namespace CatalogoApi.Application.Interfaces;

public interface IFilmeService
{
    IReadOnlyList<Filme> ListarTodos(Genero? genero = null, ClassificacaoEtaria? classificacao = null);
    Filme ObterPorId(Guid id);
    Filme Criar(string titulo, Genero genero, int anoLancamento, string? sinopse, decimal avaliacao, int duracaoMinutos, string diretor, ClassificacaoEtaria classificacao);
    Filme Atualizar(Guid id, string titulo, Genero genero, int anoLancamento, string? sinopse, decimal avaliacao, int duracaoMinutos, string diretor, ClassificacaoEtaria classificacao);
    void Deletar(Guid id);
}
