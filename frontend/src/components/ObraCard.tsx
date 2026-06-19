import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { Heart, Play, Film, Tv, Star } from 'lucide-react'
import type { ObraResumo } from '../types'
import Badge from './ui/Badge'
import StarRating from './StarRating'

const genreColors: Record<string, [string, string]> = {
  Acao: ['#4a0000', '#7c2d00'],
  Comedia: ['#3b2000', '#4d2e00'],
  Drama: ['#0a1628', '#0c2340'],
  Terror: ['#150020', '#1a0030'],
  Romance: ['#3b0020', '#500030'],
  SciFi: ['#001428', '#002040'],
  Documentario: ['#002018', '#003020'],
  Animacao: ['#2a0040', '#380050'],
  Thriller: ['#1a1a1a', '#0f0f1a'],
  Aventura: ['#002a10', '#003818'],
  Fantasia: ['#1a0038', '#280050'],
  Crime: ['#1a1410', '#252015'],
}

interface ObraCardProps {
  obra: ObraResumo
  isFavorito?: boolean
  onToggleFavorito?: (obraId: string) => void
  onAvaliar?: (obra: ObraResumo) => void
}

export default function ObraCard({
  obra,
  isFavorito = false,
  onToggleFavorito,
  onAvaliar,
}: ObraCardProps) {
  const navigate = useNavigate()
  const [heartPulse, setHeartPulse] = useState(false)

  const [from, to] = genreColors[obra.genero] ?? ['#111118', '#1C1C28']

  const handleFavorito = (e: React.MouseEvent) => {
    e.stopPropagation()
    setHeartPulse(true)
    setTimeout(() => setHeartPulse(false), 300)
    onToggleFavorito?.(obra.id)
  }

  const handleAvaliar = (e: React.MouseEvent) => {
    e.stopPropagation()
    onAvaliar?.(obra)
  }

  const handleClick = () =>
    navigate(obra.tipo === 'Filme' ? `/filmes/${obra.id}` : `/series/${obra.id}`)

  return (
    <div
      onClick={handleClick}
      className="group relative cursor-pointer rounded-xl overflow-hidden bg-cinema-surface border border-cinema-border hover:border-cinema-gold/50 transition-all duration-200 hover:scale-[1.03] hover:shadow-[0_0_28px_rgba(232,184,75,0.12)]"
    >
      <div
        className="relative aspect-[2/3] flex items-center justify-center"
        style={{ background: `linear-gradient(135deg, ${from}, ${to})` }}
      >
        <div className="opacity-15 group-hover:opacity-25 transition-opacity">
          {obra.tipo === 'Filme' ? <Film size={52} color="#fff" /> : <Tv size={52} color="#fff" />}
        </div>

        <div className="absolute inset-0 bg-black/60 opacity-0 group-hover:opacity-100 transition-opacity flex items-center justify-center">
          <span className="flex items-center gap-1.5 bg-cinema-gold text-black px-4 py-2 rounded-full font-semibold text-xs">
            <Play size={12} fill="black" /> Ver detalhes
          </span>
        </div>

        <div className="absolute top-2 left-2">
          <Badge variant={obra.tipo === 'Filme' ? 'blue' : 'purple'}>
            {obra.tipo === 'Filme' ? 'FILME' : 'SÉRIE'}
          </Badge>
        </div>

        {onToggleFavorito && (
          <button
            onClick={handleFavorito}
            className="absolute top-2 right-2 p-1.5 rounded-full bg-black/50 hover:bg-black/70 transition-colors"
            title={isFavorito ? 'Remover dos favoritos' : 'Adicionar aos favoritos'}
          >
            <Heart
              size={15}
              className={`transition-transform duration-150 ${heartPulse ? 'scale-125' : 'scale-100'} ${
                isFavorito ? 'fill-cinema-red text-cinema-red' : 'text-white'
              }`}
            />
          </button>
        )}
      </div>

      <div className="p-3">
        <h3 className="font-display text-lg leading-tight text-cinema-primary truncate tracking-wide">
          {obra.titulo}
        </h3>

        <p className="text-cinema-muted text-xs mt-0.5 truncate">
          {obra.anoLancamento} · {obra.genero}
        </p>

        <div className="flex items-center justify-between gap-2 mt-1.5">
          <StarRating value={obra.avaliacao} />

          {onAvaliar && (
            <button
              onClick={handleAvaliar}
              className="flex items-center gap-1 text-[11px] text-cinema-gold hover:brightness-125 transition-all"
              title="Avaliar obra"
            >
              <Star size={12} />
              Avaliar
            </button>
          )}
        </div>
      </div>
    </div>
  )
}