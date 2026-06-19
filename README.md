# CineLog — Catálogo de Filmes e Séries

Sistema desenvolvido em **C#** para a disciplina de **Programação Orientada por Objetos**, com o objetivo de gerenciar um catálogo de filmes e séries, permitindo cadastro de obras, usuários, favoritos e avaliações.

O projeto aplica conceitos de **Programação Orientada a Objetos**, como **abstração, encapsulamento, herança e polimorfismo**, além de utilizar boas práticas como separação em camadas, DTOs, repositórios, serviços, tratamento global de exceções e persistência de dados em arquivos JSON.

---
## Autores

Projeto desenvolvido em grupo para a disciplina de **Programação Orientada por Objetos**.

Integrantes:

* Bernardo Lomas
* Daniel Meireles
* Lucas Adelino
* Matheus Henrique

---

## Objetivo do projeto

O CineLog é um sistema de organização de mídias audiovisuais. Ele permite que usuários cadastrem e consultem filmes e séries, filtrem obras por tipo e gênero, adicionem obras aos favoritos e realizem avaliações com nota e comentário.

O sistema foi baseado no tema **Catálogo de Filmes e Séries**, proposto para o trabalho prático da disciplina.

---

## Funcionalidades principais

* Cadastro, listagem, edição e exclusão de filmes;
* Cadastro, listagem, edição e exclusão de séries;
* Cadastro, listagem, edição e exclusão de usuários;
* Listagem geral do catálogo;
* Filtro por tipo de obra: filme ou série;
* Filtro por gênero;
* Busca por título;
* Seleção de usuário ativo;
* Adição e remoção de obras favoritas por usuário;
* Listagem de favoritos por usuário;
* Cadastro de avaliações com nota e comentário;
* Regra que impede o mesmo usuário de avaliar a mesma obra mais de uma vez;
* Recalculo da média da obra após nova avaliação;
* Persistência dos dados em arquivos JSON;
* Interface web para navegação;
* API documentada com Swagger.

---

## Tecnologias utilizadas

### Backend

* C#
* .NET 9
* ASP.NET Core Web API
* Swagger
* System.Text.Json

### Frontend

* React
* TypeScript
* Vite
* Axios
* React Router
* Tailwind CSS
* Lucide React

### Persistência

Os dados são armazenados em arquivos `.json`, localizados na pasta:

```text
src/catalogo-api/Data
```

Arquivos utilizados:

```text
filmes.json
series.json
usuarios.json
avaliacoes.json
```

---

## Estrutura do projeto

```text
catalogo-filmes-series/
├── frontend/
│   ├── src/
│   │   ├── api/
│   │   ├── components/
│   │   ├── contexts/
│   │   ├── hooks/
│   │   ├── pages/
│   │   └── types/
│   └── package.json
│
├── src/
│   └── catalogo-api/
│       ├── Api/
│       │   ├── Controllers/
│       │   ├── Dtos/
│       │   └── Middleware/
│       ├── Application/
│       │   ├── Interfaces/
│       │   └── Services/
│       ├── Domain/
│       │   ├── Entities/
│       │   ├── Enums/
│       │   └── Exceptions/
│       ├── Infrastructure/
│       │   ├── Interfaces/
│       │   └── Repositories/
│       ├── Data/
│       ├── Program.cs
│       └── catalogo-api.csproj
│
├── design/
├── README.md
└── .gitignore
```

---

## Como executar o projeto

### Pré-requisitos

Antes de executar, é necessário ter instalado:

* .NET SDK 9;
* Node.js;
* npm;
* Git.

Para conferir se o .NET está instalado:

```powershell
dotnet --version
```

Para conferir se o Node.js está instalado:

```powershell
node --version
```

Para conferir se o npm está instalado:

```powershell
npm --version
```

---

## 1. Clonar o repositório

```powershell
git clone https://github.com/lucasadelino-dev/catalogo-filmes-series.git
```

Depois, entre na pasta do projeto:

```powershell
cd catalogo-filmes-series
```

---

## 2. Executar o backend

Entre na pasta da API:

```powershell
cd src/catalogo-api
```

Restaure as dependências:

```powershell
dotnet restore
```

Compile o projeto:

```powershell
dotnet build
```

Execute a API:

```powershell
dotnet run
```

A API ficará disponível em:

```text
http://localhost:5136
```

A documentação Swagger ficará disponível em:

```text
http://localhost:5136/swagger
```

---

## 3. Executar o frontend

Abra outro terminal na pasta principal do projeto e entre na pasta do frontend:

```powershell
cd frontend
```

Instale as dependências:

```powershell
npm install
```

Execute o frontend:

```powershell
npm run dev
```

O frontend ficará disponível em:

```text
http://localhost:5173
```

---

## Configuração do frontend

O frontend utiliza a variável `VITE_API_URL` para saber o endereço da API.

Na pasta `frontend`, o arquivo `.env` deve conter:

```env
VITE_API_URL=http://localhost:5136
```

Caso a porta da API seja alterada, esse valor também deve ser atualizado.

---

## Rotas principais da API

### Catálogo

```text
GET /catalogo
GET /api/catalogo
```

Lista todas as obras cadastradas no catálogo.

---

### Filmes

```text
GET    /filmes
GET    /api/filmes
POST   /filmes
POST   /api/filmes
PUT    /filmes/{id}
PUT    /api/filmes/{id}
DELETE /filmes/{id}
DELETE /api/filmes/{id}
```

---

### Séries

```text
GET    /series
GET    /api/series
POST   /series
POST   /api/series
PUT    /series/{id}
PUT    /api/series/{id}
DELETE /series/{id}
DELETE /api/series/{id}
```

---

### Usuários

```text
GET    /usuarios
GET    /api/usuarios
POST   /usuarios
POST   /api/usuarios
PUT    /usuarios/{id}
PUT    /api/usuarios/{id}
DELETE /usuarios/{id}
DELETE /api/usuarios/{id}
```

---

### Favoritos

```text
GET    /usuarios/{usuarioId}/favoritos
GET    /api/usuarios/{usuarioId}/favoritos
POST   /usuarios/{usuarioId}/favoritos/{obraId}
POST   /api/usuarios/{usuarioId}/favoritos/{obraId}
DELETE /usuarios/{usuarioId}/favoritos/{obraId}
DELETE /api/usuarios/{usuarioId}/favoritos/{obraId}
```

---

### Avaliações

```text
GET  /avaliacoes/obra/{obraId}
GET  /api/avaliacoes/obra/{obraId}
GET  /avaliacoes/usuario/{usuarioId}
GET  /api/avaliacoes/usuario/{usuarioId}
POST /avaliacoes/usuarios/{usuarioId}/obras/{obraId}
POST /api/avaliacoes/usuarios/{usuarioId}/obras/{obraId}
```

Exemplo de corpo para cadastrar uma avaliação:

```json
{
  "nota": 8.5,
  "comentario": "Ótima obra, recomendo."
}
```

---

## Aplicação dos pilares da POO

### Abstração

A abstração aparece na classe `ObraAudiovisual`, que representa características comuns entre filmes e séries, como título, gênero, ano de lançamento, sinopse e avaliação.

Classes como `Filme` e `Serie` herdam essa estrutura comum e adicionam seus próprios atributos.

---

### Encapsulamento

As entidades do domínio protegem seus dados com propriedades de alteração restrita, como `private set`, e expõem métodos específicos para modificar o estado interno.

Exemplos:

* `AdicionarFavorito`;
* `RemoverFavorito`;
* `Atualizar`;
* `AtualizarAvaliacao`.

Isso evita alterações diretas e mantém as regras de negócio dentro das classes.

---

### Herança

A herança é aplicada por meio da relação entre:

```text
ObraAudiovisual
├── Filme
└── Serie
```

`Filme` e `Serie` reutilizam os atributos e comportamentos da classe base `ObraAudiovisual`, mas também possuem características próprias.

---

### Polimorfismo

O polimorfismo aparece no método `ObterDetalhes()`.

A classe base define o comportamento abstrato, e cada classe filha implementa sua própria versão:

* `Filme.ObterDetalhes()`;
* `Serie.ObterDetalhes()`.

Assim, o sistema pode tratar filmes e séries como obras audiovisuais, mas cada tipo exibe seus detalhes de forma específica.

---

## Regras de negócio implementadas

* Uma obra pode ser filme ou série;
* Filmes possuem duração, diretor e classificação etária;
* Séries possuem número de temporadas, episódios por temporada, criador e status;
* Usuários podem adicionar obras aos favoritos;
* Uma obra não pode ser adicionada duas vezes aos favoritos do mesmo usuário;
* Usuários podem avaliar obras com nota e comentário;
* Um usuário não pode avaliar a mesma obra mais de uma vez;
* A média da obra é recalculada após cada nova avaliação;
* E-mails de usuários não podem ser repetidos;
* Dados inválidos são bloqueados por validações no domínio e na API.

---

## Validações e tratamento de exceções

O sistema possui validações para evitar dados inválidos, como:

* título obrigatório;
* ano de lançamento válido;
* avaliação entre 0 e 10;
* duração maior que zero;
* nome de usuário obrigatório;
* e-mail válido;
* comentário de avaliação com limite de caracteres;
* bloqueio de avaliação duplicada;
* bloqueio de favorito duplicado.

O tratamento de erros é centralizado em um middleware global, responsável por retornar respostas padronizadas para erros de domínio, conflitos, recursos não encontrados e erros internos.

---

## Padrões e boas práticas utilizados

### Repository Pattern

Os repositórios isolam a lógica de persistência dos dados. Assim, as camadas de serviço não precisam saber diretamente como os dados são salvos.

Exemplos:

* `FilmeRepository`;
* `SerieRepository`;
* `UsuarioRepository`;
* `AvaliacaoRepository`.

---

### Service Layer

Os serviços concentram as regras de negócio do sistema.

Exemplos:

* `FilmeService`;
* `SerieService`;
* `UsuarioService`;
* `CatalogoService`;
* `AvaliacaoService`.

---

### DTOs

A API utiliza DTOs para separar os dados recebidos e enviados pela aplicação das entidades de domínio.

Exemplos:

* `CriarFilmeRequest`;
* `CriarSerieRequest`;
* `CriarUsuarioRequest`;
* `CriarAvaliacaoRequest`;
* `FilmeResponse`;
* `SerieResponse`;
* `AvaliacaoResponse`.

---

### Middleware de exceções

O `GlobalExceptionMiddleware` centraliza o tratamento de erros da aplicação e evita repetição de código nos controllers.

---

### Injeção de dependência

Os services e repositories são registrados no `Program.cs` e injetados nas classes que dependem deles.

Isso reduz acoplamento e melhora a organização do projeto.

---

## Como testar o sistema

### Teste pelo frontend

1. Execute o backend;
2. Execute o frontend;
3. Acesse `http://localhost:5173`;
4. Entre na aba **Admin**;
5. Cadastre um usuário;
6. Cadastre um filme ou uma série;
7. Volte para o catálogo;
8. Selecione o usuário ativo;
9. Adicione uma obra aos favoritos;
10. Acesse a aba **Favoritos**;
11. Confirme se a obra aparece;
12. Volte para o catálogo;
13. Clique em **Avaliar**;
14. Informe uma nota e um comentário;
15. Confirme se a avaliação foi salva e a média foi atualizada.

---

### Teste pelo Swagger

1. Execute a API;
2. Acesse `http://localhost:5136/swagger`;
3. Use `GET /usuarios` para obter um usuário;
4. Use `GET /catalogo` para obter uma obra;
5. Use `POST /api/avaliacoes/usuarios/{usuarioId}/obras/{obraId}` para cadastrar uma avaliação;
6. Tente avaliar a mesma obra com o mesmo usuário novamente;
7. O sistema deve retornar erro de conflito, impedindo avaliação duplicada.

---

## Observações sobre armazenamento

Os arquivos JSON são criados automaticamente na primeira execução da API, caso não existam.

Ao cadastrar, editar ou excluir dados, os arquivos são atualizados automaticamente.

Isso permite que os dados continuem disponíveis mesmo após encerrar e executar o sistema novamente.

---
