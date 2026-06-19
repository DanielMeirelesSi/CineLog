using System.Text.Json;
using System.Text.Json.Serialization;
using CatalogoApi.Domain.Entities;
using CatalogoApi.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace CatalogoApi.Infrastructure.Repositories;

public class SerieRepository : IRepository<Serie>
{
    private readonly Dictionary<Guid, Serie> _store = [];
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions;

    public SerieRepository(IWebHostEnvironment environment)
    {
        _filePath = Path.Combine(environment.ContentRootPath, "Data", "series.json");

        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        _jsonOptions.Converters.Add(new JsonStringEnumConverter());

        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
        CarregarDados();
    }

    public Serie? GetById(Guid id) => _store.GetValueOrDefault(id);

    public IReadOnlyList<Serie> GetAll() => [.. _store.Values];

    public void Add(Serie entity)
    {
        _store[entity.Id] = entity;
        SalvarDados();
    }

    public void Update(Serie entity)
    {
        _store[entity.Id] = entity;
        SalvarDados();
    }

    public void Remove(Guid id)
    {
        _store.Remove(id);
        SalvarDados();
    }

    public bool Exists(Guid id) => _store.ContainsKey(id);

    private void CarregarDados()
    {
        if (!File.Exists(_filePath))
            return;

        string json = File.ReadAllText(_filePath);

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<Serie>? series = JsonSerializer.Deserialize<List<Serie>>(json, _jsonOptions);

        if (series is null)
            return;

        foreach (Serie serie in series)
            _store[serie.Id] = serie;
    }

    private void SalvarDados()
    {
        List<Serie> series = [.. _store.Values];
        string json = JsonSerializer.Serialize(series, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }
}