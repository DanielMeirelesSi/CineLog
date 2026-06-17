import type { Genero } from '../types'

export const GENERO_LABELS: Record<Genero, string> = {
  Acao: 'Ação', Comedia: 'Comédia', Drama: 'Drama', Terror: 'Terror',
  Romance: 'Romance', SciFi: 'Sci-Fi', Documentario: 'Documentário',
  Animacao: 'Animação', Thriller: 'Thriller', Aventura: 'Aventura',
  Fantasia: 'Fantasia', Crime: 'Crime',
}

const GENEROS: Genero[] = [
  'Acao', 'Comedia', 'Drama', 'Terror', 'Romance', 'SciFi',
  'Documentario', 'Animacao', 'Thriller', 'Aventura', 'Fantasia', 'Crime',
]

interface FilterChipsProps {
  tipo: string
  genero: string
  onTipoChange: (tipo: string) => void
  onGeneroChange: (genero: Genero | '') => void
}

const chip = (active: boolean) =>
  `px-4 py-1.5 rounded-full text-sm font-medium transition-all duration-150 whitespace-nowrap border cursor-pointer ` +
  (active
    ? 'bg-cinema-gold text-black border-cinema-gold font-semibold'
    : 'bg-cinema-elevated text-cinema-muted border-cinema-border hover:border-cinema-gold/50 hover:text-cinema-primary')

export default function FilterChips({ tipo, genero, onTipoChange, onGeneroChange }: FilterChipsProps) {
  return (
    <div className="flex flex-col gap-3">
      <div className="flex items-center gap-2 overflow-x-auto scrollbar-hide pb-0.5">
        <span className="text-cinema-muted text-xs shrink-0 font-medium">Tipo:</span>
        {(['', 'Filme', 'Serie'] as const).map(t => (
          <button key={t} onClick={() => onTipoChange(t)} className={chip(tipo === t)}>
            {t === '' ? 'Todos' : t === 'Filme' ? 'Filmes' : 'Séries'}
          </button>
        ))}
      </div>
      <div className="flex items-center gap-2 overflow-x-auto scrollbar-hide pb-0.5">
        <span className="text-cinema-muted text-xs shrink-0 font-medium">Gênero:</span>
        <button onClick={() => onGeneroChange('')} className={chip(genero === '')}>Todos</button>
        {GENEROS.map(g => (
          <button key={g} onClick={() => onGeneroChange(genero === g ? '' : g)} className={chip(genero === g)}>
            {GENERO_LABELS[g]}
          </button>
        ))}
      </div>
    </div>
  )
}
