using System.Text.Json;
using System.Text.Json.Serialization;
using CatalogoApi.Domain.Entities;
using CatalogoApi.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace CatalogoApi.Infrastructure.Repositories;

public class AvaliacaoRepository : IRepository<Avaliacao>
{
    private readonly Dictionary<Guid, Avaliacao> _store = [];
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions;

    public AvaliacaoRepository(IWebHostEnvironment environment)
    {
        _filePath = Path.Combine(environment.ContentRootPath, "Data", "avaliacoes.json");

        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        _jsonOptions.Converters.Add(new JsonStringEnumConverter());

        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
        CarregarDados();
    }

    public Avaliacao? GetById(Guid id)
    {
        return _store.GetValueOrDefault(id);
    }

    public IReadOnlyList<Avaliacao> GetAll()
    {
        return [.. _store.Values];
    }

    public void Add(Avaliacao entity)
    {
        _store[entity.Id] = entity;
        SalvarDados();
    }

    public void Update(Avaliacao entity)
    {
        _store[entity.Id] = entity;
        SalvarDados();
    }

    public void Remove(Guid id)
    {
        _store.Remove(id);
        SalvarDados();
    }

    public bool Exists(Guid id)
    {
        return _store.ContainsKey(id);
    }

    private void CarregarDados()
    {
        if (!File.Exists(_filePath))
            return;

        string json = File.ReadAllText(_filePath);

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<Avaliacao>? avaliacoes = JsonSerializer.Deserialize<List<Avaliacao>>(json, _jsonOptions);

        if (avaliacoes is null)
            return;

        foreach (Avaliacao avaliacao in avaliacoes)
            _store[avaliacao.Id] = avaliacao;
    }

    private void SalvarDados()
    {
        List<Avaliacao> avaliacoes = [.. _store.Values];
        string json = JsonSerializer.Serialize(avaliacoes, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }
}