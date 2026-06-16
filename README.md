# Catálogo de Filmes e Séries

## Integrantes

- Bernardo Maia Lomas Ameno
- Daniel Meireles Aquino Jorge
- Lucas Gabriel Adelino Araújo
- Matheus Henrique Borges Ferreira

## Descrição

Sistema de catálogo de filmes e séries desenvolvido como Trabalho Prático da disciplina de Programação Orientada a Objetos. O sistema permite gerenciar e consultar obras audiovisuais, expondo informações como título, gênero, ano de lançamento e avaliações.

O projeto aplica os quatro pilares da POO:

- **Abstração** — entidades de domínio representam obras do mundo real
- **Encapsulamento** — estado interno protegido por propriedades e serviços
- **Herança** — `Filme` e `Serie` herdam de uma base comum `Obra`
- **Polimorfismo** — comportamentos específicos sobrescritos em cada subclasse

## Funcionalidades

- Cadastro de filmes e séries
- Consulta e listagem de obras
- Pesquisa por título
- Filtragem por gênero
- Gerenciamento de favoritos
- Exibição de detalhes da obra

## Tecnologias

- **C# / ASP.NET Core** — API REST
- **Programação Orientada a Objetos**
- **.NET** (runtime e SDK)
- **Git e GitHub** — versionamento

## Estrutura do Projeto

```
catalogo-filmes-series/
├── src/
│   └── catalogo-api/
│       ├── Api/              # Controllers, DTOs, Middleware
│       ├── Application/      # Interfaces e Services
│       ├── Domain/           # Entidades, Enums, Exceptions
│       └── Infrastructure/   # Repositories
└── tests/                    # Testes automatizados
```

### Entidades principais

- `Obra` — classe base abstrata
- `Filme` — herda de Obra
- `Serie` — herda de Obra
- `Usuario`
- `Catalogo`

## Como executar

```bash
cd src/catalogo-api
dotnet run
```

A API estará disponível em `https://localhost:5001` (ou conforme `appsettings.json`).
