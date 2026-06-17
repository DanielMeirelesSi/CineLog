# Frontend Spec — Catálogo de Filmes e Séries

## Stack

| Camada | Lib |
|---|---|
| Framework | React 18 + Vite |
| Linguagem | TypeScript |
| Roteamento | React Router v6 |
| Estilo | Tailwind CSS v3 |
| Ícones | Lucide React |
| HTTP | Axios |
| Fontes | Bebas Neue (display) · Inter (corpo) |

---

## Paleta — Cores Cinemáticas

| Token | Hex | Uso |
|---|---|---|
| `bg-base` | `#08080F` | Fundo geral |
| `bg-surface` | `#111118` | Cards, painéis |
| `bg-elevated` | `#1C1C28` | Hover, inputs, modais |
| `accent-gold` | `#E8B84B` | CTA primário, destaques |
| `accent-red` | `#C0392B` | Favoritos, badges de exclusão |
| `accent-blue` | `#2E86C1` | Links, badges de série |
| `text-primary` | `#F0F0F5` | Títulos e corpo principal |
| `text-muted` | `#7A7A96` | Subtexto, placeholders |
| `border` | `#2A2A3C` | Bordas de cards e inputs |

> Gradiente hero: `from-[#08080F] via-[#12121E] to-[#1a0a1e]`  
> Glow dourado: `box-shadow: 0 0 24px rgba(232,184,75,0.15)`

---

## Tipografia

```
Bebas Neue  → títulos de página, nome da obra no card (letter-spacing: 0.05em)
Inter       → tudo mais (400 / 500 / 600 / 700)
```

Escala (Tailwind):

| Classe | Uso |
|---|---|
| `text-5xl font-display` | Hero title |
| `text-2xl font-display` | Título da página |
| `text-lg font-semibold` | Título do card |
| `text-sm text-muted` | Metadados (ano, gênero) |

---

## Layout Global

```
┌─────────────────────────────────────────────┐
│  HEADER (sticky, blur backdrop)             │
│  Logo  │  Nav: Catálogo · Séries · Favoritos│  Search icon  Avatar │
├─────────────────────────────────────────────┤
│                                             │
│               PAGE CONTENT                 │
│                                             │
├─────────────────────────────────────────────┤
│  FOOTER  —  © 2025 · PUC · POO             │
└─────────────────────────────────────────────┘
```

### Header

- `sticky top-0 z-50`  
- Fundo: `bg-[#08080F]/80 backdrop-blur-md border-b border-[#2A2A3C]`
- Logo: ícone `Clapperboard` (Lucide) + texto **"CINELOG"** em Bebas Neue
- Nav links com underline animado `gold` no hover/active
- Ícone `Search` abre barra de busca fullwidth com slide-down
- Avatar clicável abre dropdown com link para Favoritos

---

## Páginas

### 1. Catálogo — `/`

**Layout:**

```
┌──────────────────────────────────────────────────┐
│  HERO BANNER                                     │
│  "Descubra sua próxima obra favorita"            │
│  [SearchBar centralizada]                        │
├──────────────────────────────────────────────────┤
│  FILTROS (chips horizontais, scroll)             │
│  Tipo: Todos · Filmes · Séries                   │
│  Gênero: Ação · Drama · Sci-Fi · ... (chips)     │
├──────────────────────────────────────────────────┤
│  GRID DE CARDS                                   │
│  grid-cols-2 sm:3 md:4 lg:5  gap-4              │
└──────────────────────────────────────────────────┘
```

**ObraCard:**

```
┌──────────────┐
│  [Poster     │  ← aspect-ratio 2/3, object-cover
│   placeholder│     gradiente de filme no hover
│   com ícone] │
│              │
│ ████ Título  │  ← Bebas Neue, truncate
│ 2023 · Drama │  ← text-muted text-xs
│ ★ 8.7        │  ← accent-gold
│ [♥] [···]    │  ← favoritar + menu
└──────────────┘
```

- Hover: `scale-105`, glow dourado, overlay com botão "Ver detalhes"
- Badge no canto superior direito: `FILME` (accent-blue) ou `SÉRIE` (roxo)
- Coração preenchido = já favoritado (accent-red)

**SearchBar:**

- Input `rounded-full bg-elevated border border-[#2A2A3C]`
- Ícone `Search` à esquerda, `X` para limpar à direita
- Busca em tempo real com debounce 300ms
- Resultados sem reload (query param `?titulo=`)

**Chips de filtro:**

- `rounded-full px-4 py-1.5 text-sm`
- Inativo: `bg-elevated text-muted`
- Ativo: `bg-accent-gold text-black font-semibold`
- Transition suave no toggle

---

### 2. Detalhe da Obra — `/filmes/:id` · `/series/:id`

```
┌───────────────────────────────────────────────────┐
│ BACKDROP full-width (placeholder com gradiente)   │
│   ┌──────────┐  Título (Bebas Neue 4xl)           │
│   │  Poster  │  Gênero · Ano · Avaliação ★        │
│   │          │  Classificação (badge)             │
│   │          │  Sinopse (max 3 linhas, "ver mais")│
│   └──────────│  [♥ Favoritar]  [← Voltar]        │
├───────────────────────────────────────────────────┤
│ DETALHES TÉCNICOS  (grid 2 cols)                  │
│  Diretor / Criador   │  Duração / Temporadas      │
│  Classificação       │  Status (só série)         │
├───────────────────────────────────────────────────┤
│ OBRAS RELACIONADAS  (mesmo gênero, carrossel)     │
└───────────────────────────────────────────────────┘
```

- Backdrop: `linear-gradient(to right, #08080F 40%, transparent)` sobre placeholder
- Badge de classificação: cores por faixa (verde=Livre, amarelo=10/12, laranja=14/16, vermelho=18)
- Botão Favoritar: toggle com animação de coração (`fill` animation)
- Carrossel de relacionadas: scroll horizontal com setas `ChevronLeft/Right`

---

### 3. Favoritos — `/favoritos`

```
┌────────────────────────────────────────────────┐
│  ♥  Meus Favoritos                 [Usuário ▾] │
├────────────────────────────────────────────────┤
│  [Select usuário]  (dropdown)                  │
├────────────────────────────────────────────────┤
│  GRID igual ao catálogo (sem filtros de tipo)  │
│  Card com botão "Remover" em accent-red        │
├────────────────────────────────────────────────┤
│  Estado vazio:  ícone Heart + "Nenhum favorito │
│  ainda. Explore o catálogo!"  + botão CTA      │
└────────────────────────────────────────────────┘
```

---

### 4. Gerenciar Obras — `/admin`

```
┌─────────────────────────────────────────────────┐
│  TABS:  [ Filmes ]  [ Séries ]  [ Usuários ]    │
├─────────────────────────────────────────────────┤
│  [+ Novo Filme]                        [Buscar] │
├─────────────────────────────────────────────────┤
│  TABELA                                         │
│  Título │ Gênero │ Ano │ Avaliação │ Ações       │
│  ───────┼────────┼─────┼──────────┼──────────   │
│  Matrix │ Sci-Fi │1999 │ ★ 8.7    │ ✏️  🗑️      │
└─────────────────────────────────────────────────┘
```

**Modal de criação/edição** (slide-in do lado direito, `w-[480px]`):

```
┌────────────────────────────────┐
│ Novo Filme               [✕]  │
├────────────────────────────────┤
│ Título *                       │
│ [________________________]     │
│ Gênero *        Ano *          │
│ [Select ▾]      [____]         │
│ Avaliação *     Duração (min)* │
│ [0.0 – 10.0]    [____]         │
│ Diretor *                      │
│ [________________________]     │
│ Classificação *                │
│ [Select ▾]                     │
│ Sinopse                        │
│ [________________________]     │
│ [________________________]     │
│                                │
│        [Cancelar] [Salvar]     │
└────────────────────────────────┘
```

- Inputs: `bg-elevated border border-[#2A2A3C] rounded-lg focus:border-accent-gold`
- Botão Salvar: `bg-accent-gold text-black font-semibold hover:brightness-110`
- Validação em tempo real com mensagens inline (vermelho accent-red)
- Confirmação de exclusão: mini modal com texto e dois botões

---

## Componentes Compartilhados

| Componente | Descrição |
|---|---|
| `<Header />` | Sticky, nav, busca, avatar |
| `<ObraCard />` | Card do grid com hover, badge, favorito |
| `<SearchBar />` | Input com debounce |
| `<FilterChips />` | Chips de tipo e gênero |
| `<StarRating />` | Exibe avaliação com estrelas |
| `<Badge />` | Badge colorido (tipo, classificação, status) |
| `<Modal />` | Overlay centralizado com backdrop blur |
| `<Drawer />` | Slide-in lateral para formulários |
| `<EmptyState />` | Ícone + mensagem + CTA para listas vazias |
| `<Spinner />` | Loading ring em accent-gold |
| `<Toast />` | Notificação no canto inferior direito |

---

## Interações e Animações

| Elemento | Animação |
|---|---|
| Card hover | `scale-105 transition-transform duration-200` |
| Card glow | `box-shadow` dourado no hover |
| Chip toggle | `transition-colors duration-150` |
| Favoritar | Coração pulsa (`scale 1 → 1.3 → 1`) em accent-red |
| Drawer | `translate-x-full → translate-x-0` 250ms ease-out |
| Modal | `opacity-0 scale-95 → opacity-100 scale-100` |
| Toast | Slide-up + fade, auto-dismiss 3s |
| Busca header | Slide-down + fade 200ms |
| Skeleton loader | Pulse em bg-elevated antes dos dados chegarem |

---

## Responsividade

| Breakpoint | Comportamento |
|---|---|
| `< 640px` | Grid 2 cols · Nav vira hamburger menu |
| `640–1024px` | Grid 3 cols · Header completo |
| `> 1024px` | Grid 4–5 cols · Drawer lateral no admin |

---

## Integração com a API

Base URL configurada via `VITE_API_URL` no `.env`.

| Página | Chamadas |
|---|---|
| Catálogo | `GET /catalogo`, `GET /catalogo/buscar`, `GET /catalogo/genero/:genero` |
| Detalhe Filme | `GET /filmes/:id` |
| Detalhe Série | `GET /series/:id` |
| Favoritos | `GET /usuarios/:id/favoritos`, `POST /usuarios/:id/favoritos/:obraId`, `DELETE /usuarios/:id/favoritos/:obraId` |
| Admin Filmes | `GET /filmes`, `POST /filmes`, `PUT /filmes/:id`, `DELETE /filmes/:id` |
| Admin Séries | `GET /series`, `POST /series`, `PUT /series/:id`, `DELETE /series/:id` |
| Admin Usuários | `GET /usuarios`, `POST /usuarios`, `PUT /usuarios/:id`, `DELETE /usuarios/:id` |

---

## Estrutura de Pastas (React + Vite)

```
src/
├── api/
│   ├── axios.ts          # instância com baseURL
│   ├── filmes.ts
│   ├── series.ts
│   ├── usuarios.ts
│   └── catalogo.ts
├── components/
│   ├── ui/               # Badge, Modal, Drawer, Spinner, Toast
│   ├── ObraCard.tsx
│   ├── SearchBar.tsx
│   ├── FilterChips.tsx
│   └── Header.tsx
├── pages/
│   ├── Catalogo.tsx
│   ├── DetalheFilme.tsx
│   ├── DetalheSerie.tsx
│   ├── Favoritos.tsx
│   └── Admin.tsx
├── hooks/
│   ├── useCatalogo.ts
│   ├── useFavoritos.ts
│   └── useDebounce.ts
├── types/
│   └── index.ts          # interfaces: Filme, Serie, Usuario, ObraResumo
└── main.tsx
```
