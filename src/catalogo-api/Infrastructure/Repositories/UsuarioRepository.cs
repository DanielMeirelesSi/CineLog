using System.Text.Json;
using System.Text.Json.Serialization;
using CatalogoApi.Domain.Entities;
using CatalogoApi.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace CatalogoApi.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly Dictionary<Guid, Usuario> _store = [];
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions;

    public UsuarioRepository(IWebHostEnvironment environment)
    {
        _filePath = Path.Combine(environment.ContentRootPath, "Data", "usuarios.json");

        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        _jsonOptions.Converters.Add(new JsonStringEnumConverter());

        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
        CarregarDados();
    }

    public Usuario? GetById(Guid id) => _store.GetValueOrDefault(id);

    public IReadOnlyList<Usuario> GetAll() => [.. _store.Values];

    public void Add(Usuario entity)
    {
        _store[entity.Id] = entity;
        SalvarDados();
    }

    public void Update(Usuario entity)
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

    public bool EmailJaCadastrado(string email, Guid? excluirId = null) =>
        _store.Values.Any(u =>
            string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase)
            && u.Id != excluirId);

    private void CarregarDados()
    {
        if (!File.Exists(_filePath))
            return;

        string json = File.ReadAllText(_filePath);

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<Usuario>? usuarios = JsonSerializer.Deserialize<List<Usuario>>(json, _jsonOptions);

        if (usuarios is null)
            return;

        foreach (Usuario usuario in usuarios)
            _store[usuario.Id] = usuario;
    }

    private void SalvarDados()
    {
        List<Usuario> usuarios = [.. _store.Values];
        string json = JsonSerializer.Serialize(usuarios, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }
}