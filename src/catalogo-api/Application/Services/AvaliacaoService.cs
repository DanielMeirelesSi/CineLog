using CatalogoApi.Application.Interfaces;
using CatalogoApi.Domain.Entities;
using CatalogoApi.Domain.Exceptions;
using CatalogoApi.Infrastructure.Interfaces;

namespace CatalogoApi.Application.Services;

public class AvaliacaoService(
    IRepository<Avaliacao> avaliacaoRepository,
    IRepository<Filme> filmeRepository,
    IRepository<Serie> serieRepository,
    IUsuarioRepository usuarioRepository,
    ICatalogoService catalogoService) : IAvaliacaoService
{
    public IReadOnlyList<Avaliacao> ListarPorObra(Guid obraId)
    {
        catalogoService.ObterPorId(obraId);

        return avaliacaoRepository.GetAll()
            .Where(a => a.ObraId == obraId)
            .OrderByDescending(a => a.CreatedAt)
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyList<Avaliacao> ListarPorUsuario(Guid usuarioId)
    {
        if (!usuarioRepository.Exists(usuarioId))
            throw new NotFoundException($"Usuário com id '{usuarioId}' não encontrado.");

        return avaliacaoRepository.GetAll()
            .Where(a => a.UsuarioId == usuarioId)
            .OrderByDescending(a => a.CreatedAt)
            .ToList()
            .AsReadOnly();
    }

    public Avaliacao Avaliar(Guid usuarioId, Guid obraId, decimal nota, string? comentario)
    {
        if (!usuarioRepository.Exists(usuarioId))
            throw new NotFoundException($"Usuário com id '{usuarioId}' não encontrado.");

        catalogoService.ObterPorId(obraId);

        bool usuarioJaAvaliou = avaliacaoRepository.GetAll()
            .Any(a => a.UsuarioId == usuarioId && a.ObraId == obraId);

        if (usuarioJaAvaliou)
            throw new ConflictException("Este usuário já avaliou esta obra.");

        Avaliacao avaliacao = Avaliacao.Criar(usuarioId, obraId, nota, comentario);

        avaliacaoRepository.Add(avaliacao);

        RecalcularMediaDaObra(obraId);

        return avaliacao;
    }

    private void RecalcularMediaDaObra(Guid obraId)
    {
        List<Avaliacao> avaliacoesDaObra = avaliacaoRepository.GetAll()
            .Where(a => a.ObraId == obraId)
            .ToList();

        decimal media = avaliacoesDaObra.Count == 0
            ? 0
            : Math.Round(avaliacoesDaObra.Average(a => a.Nota), 1);

        Filme? filme = filmeRepository.GetById(obraId);

        if (filme is not null)
        {
            filme.AtualizarAvaliacao(media, DateTime.UtcNow);
            filmeRepository.Update(filme);
            return;
        }

        Serie? serie = serieRepository.GetById(obraId);

        if (serie is not null)
        {
            serie.AtualizarAvaliacao(media, DateTime.UtcNow);
            serieRepository.Update(serie);
        }
    }
}