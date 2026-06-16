using CatalogoApi.Application.Interfaces;
using CatalogoApi.Domain.Entities;
using CatalogoApi.Domain.Enums;
using CatalogoApi.Domain.Exceptions;
using CatalogoApi.Infrastructure.Interfaces;

namespace CatalogoApi.Application.Services;

public class FilmeService(IRepository<Filme> filmeRepository, IRepository<Usuario> usuarioRepository) : IFilmeService
{
    public IReadOnlyList<Filme> ListarTodos(Genero? genero = null, ClassificacaoEtaria? classificacao = null)
    {
        IEnumerable<Filme> filmes = filmeRepository.GetAll();
        if (genero.HasValue) filmes = filmes.Where(f => f.Genero == genero.Value);
        if (classificacao.HasValue) filmes = filmes.Where(f => f.Classificacao == classificacao.Value);
        return filmes.ToList().AsReadOnly();
    }

    public Filme ObterPorId(Guid id) =>
        filmeRepository.GetById(id) ?? throw new NotFoundException($"Filme com id '{id}' não encontrado.");

    public Filme Criar(string titulo, Genero genero, int anoLancamento, string? sinopse, decimal avaliacao, int duracaoMinutos, string diretor, ClassificacaoEtaria classificacao)
    {
        Filme filme = new(Guid.NewGuid(), titulo, genero, anoLancamento, sinopse, avaliacao, duracaoMinutos, diretor, classificacao, DateTime.UtcNow);
        filmeRepository.Add(filme);
        return filme;
    }

    public Filme Atualizar(Guid id, string titulo, Genero genero, int anoLancamento, string? sinopse, decimal avaliacao, int duracaoMinutos, string diretor, ClassificacaoEtaria classificacao)
    {
        Filme filme = ObterPorId(id);
        filme.Atualizar(titulo, genero, anoLancamento, sinopse, avaliacao, duracaoMinutos, diretor, classificacao, DateTime.UtcNow);
        filmeRepository.Update(filme);
        return filme;
    }

    public void Deletar(Guid id)
    {
        if (!filmeRepository.Exists(id))
            throw new NotFoundException($"Filme com id '{id}' não encontrado.");

        filmeRepository.Remove(id);

        foreach (Usuario usuario in usuarioRepository.GetAll())
        {
            usuario.RemoverFavoritoSeExistir(id);
            usuarioRepository.Update(usuario);
        }
    }
}
