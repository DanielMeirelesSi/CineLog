using System.Text.Json;
using System.Text.Json.Serialization;
using CatalogoApi.Domain.Entities;
using CatalogoApi.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace CatalogoApi.Infrastructure.Repositories;

public class FilmeRepository : IRepository<Filme>
{
    private readonly Dictionary<Guid, Filme> _store = [];
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions;

    public FilmeRepository(IWebHostEnvironment environment)
    {
        _filePath = Path.Combine(environment.ContentRootPath, "Data", "filmes.json");

        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        _jsonOptions.Converters.Add(new JsonStringEnumConverter());

        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
        CarregarDados();
    }

    public Filme? GetById(Guid id) => _store.GetValueOrDefault(id);

    public IReadOnlyList<Filme> GetAll() => [.. _store.Values];

    public void Add(Filme entity)
    {
        _store[entity.Id] = entity;
        SalvarDados();
    }

    public void Update(Filme entity)
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

        List<Filme>? filmes = JsonSerializer.Deserialize<List<Filme>>(json, _jsonOptions);

        if (filmes is null)
            return;

        foreach (Filme filme in filmes)
            _store[filme.Id] = filme;
    }

    private void SalvarDados()
    {
        List<Filme> filmes = [.. _store.Values];
        string json = JsonSerializer.Serialize(filmes, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }
}