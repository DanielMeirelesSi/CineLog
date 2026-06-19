using System.Reflection;
using System.Text.Json.Serialization;
using CatalogoApi.Api.Middleware;
using CatalogoApi.Application.Interfaces;
using CatalogoApi.Application.Services;
using CatalogoApi.Domain.Entities;
using CatalogoApi.Domain.Enums;
using CatalogoApi.Infrastructure.Interfaces;
using CatalogoApi.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Catálogo de Filmes e Séries",
        Version = "v1",
        Description = "API REST para gerenciamento de catálogo audiovisual com aplicação dos quatro pilares de POO: Abstração, Encapsulamento, Herança e Polimorfismo."
    });

    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddSingleton<FilmeRepository>();
builder.Services.AddSingleton<IRepository<Filme>>(sp => sp.GetRequiredService<FilmeRepository>());

builder.Services.AddSingleton<SerieRepository>();
builder.Services.AddSingleton<IRepository<Serie>>(sp => sp.GetRequiredService<SerieRepository>());

builder.Services.AddSingleton<UsuarioRepository>();
builder.Services.AddSingleton<IUsuarioRepository>(sp => sp.GetRequiredService<UsuarioRepository>());
builder.Services.AddSingleton<IRepository<Usuario>>(sp => sp.GetRequiredService<UsuarioRepository>());

builder.Services.AddSingleton<AvaliacaoRepository>();
builder.Services.AddSingleton<IRepository<Avaliacao>>(sp => sp.GetRequiredService<AvaliacaoRepository>());

builder.Services.AddSingleton<ICatalogoService, CatalogoService>();
builder.Services.AddSingleton<IFilmeService, FilmeService>();
builder.Services.AddSingleton<ISerieService, SerieService>();
builder.Services.AddSingleton<IUsuarioService, UsuarioService>();
builder.Services.AddSingleton<IAvaliacaoService, AvaliacaoService>();

WebApplication app = builder.Build();

SeedData(app);

app.UseCors();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Catálogo API v1");
    options.RoutePrefix = "swagger";
});

app.UseAuthorization();
app.MapControllers();

app.Run();

static void SeedData(WebApplication app)
{
    IFilmeService filmeService = app.Services.GetRequiredService<IFilmeService>();
    ISerieService serieService = app.Services.GetRequiredService<ISerieService>();
    IUsuarioService usuarioService = app.Services.GetRequiredService<IUsuarioService>();

        if (filmeService.ListarTodos().Any() ||
        serieService.ListarTodos().Any() ||
        usuarioService.ListarTodos().Any())
            {
                return;
            }

    Filme matrix = filmeService.Criar("Matrix", Genero.SciFi, 1999,
        "Um hacker descobre que a realidade em que vive é uma simulação criada por máquinas.",
        8.7m, 136, "Lana Wachowski", ClassificacaoEtaria.Quatorze);

    Filme forrestGump = filmeService.Criar("Forrest Gump", Genero.Drama, 1994,
        "A jornada extraordinária de um homem simples que testemunhou os grandes eventos do século XX.",
        8.8m, 142, "Robert Zemeckis", ClassificacaoEtaria.Dez);

    Filme deVoltaParaOFuturo = filmeService.Criar("De Volta para o Futuro", Genero.Aventura, 1985,
        "Um adolescente viaja acidentalmente para o passado em um DeLorean modificado.",
        8.5m, 116, "Robert Zemeckis", ClassificacaoEtaria.Livre);

    Serie breakingBad = serieService.Criar("Breaking Bad", Genero.Drama, 2008,
        "Um professor de química diagnosticado com câncer terminal se torna fabricante de metanfetamina.",
        9.5m, 5, 13, "Vince Gilligan", StatusSerie.Finalizada);

    Serie strangerThings = serieService.Criar("Stranger Things", Genero.SciFi, 2016,
        "Um grupo de crianças investiga o desaparecimento de um amigo e enfrenta forças sobrenaturais.",
        8.7m, 4, 9, "Irmãos Duffer", StatusSerie.Finalizada);

    Serie theLastOfUs = serieService.Criar("The Last of Us", Genero.Thriller, 2023,
        "Em um mundo pós-apocalíptico tomado por infectados, um sobrevivente protege uma adolescente imune.",
        8.8m, 2, 9, "Craig Mazin", StatusSerie.EmAndamento);

    Usuario lucas = usuarioService.Criar("Lucas Adelino", "lucas@catalogo.com");
    Usuario ana = usuarioService.Criar("Ana Silva", "ana@catalogo.com");

    usuarioService.AdicionarFavorito(lucas.Id, matrix.Id);
    usuarioService.AdicionarFavorito(lucas.Id, breakingBad.Id);
    usuarioService.AdicionarFavorito(lucas.Id, theLastOfUs.Id);

    usuarioService.AdicionarFavorito(ana.Id, forrestGump.Id);
    usuarioService.AdicionarFavorito(ana.Id, strangerThings.Id);
    usuarioService.AdicionarFavorito(ana.Id, deVoltaParaOFuturo.Id);
}
