import { useEffect, useState } from 'react'
import { useParams, useNavigate } from 'react-router-dom'
import { ArrowLeft, Clock, User, Star, Film, Calendar } from 'lucide-react'
import { obterFilme } from '../api/filmes'
import { listarCatalogo } from '../api/catalogo'
import type { Filme, ObraResumo, ClassificacaoEtaria } from '../types'
import Badge from '../components/ui/Badge'
import StarRating from '../components/StarRating'
import ObraCard from '../components/ObraCard'
import Spinner from '../components/ui/Spinner'

type BadgeVariant = 'green' | 'orange' | 'red'

const classifVariant: Record<ClassificacaoEtaria, BadgeVariant> = {
  Livre: 'green', Dez: 'green', Doze: 'green',
  Quatorze: 'orange', Dezesseis: 'orange', Dezoito: 'red',
}

const classifLabel: Record<ClassificacaoEtaria, string> = {
  Livre: 'Livre', Dez: '10+', Doze: '12+',
  Quatorze: '14+', Dezesseis: '16+', Dezoito: '18+',
}

export default function DetalheFilme() {
  const { id } = useParams<{ id: string }>()
  const navigate = useNavigate()
  const [filme, setFilme] = useState<Filme | null>(null)
  const [relacionadas, setRelacionadas] = useState<ObraResumo[]>([])
  const [loading, setLoading] = useState(true)
  const [expanded, setExpanded] = useState(false)

  useEffect(() => {
    if (!id) return
    setLoading(true)
    obterFilme(id)
      .then(f => {
        setFilme(f)
        return listarCatalogo('Filme', f.genero)
      })
      .then(obras => setRelacionadas(obras.filter(o => o.id !== id).slice(0, 6)))
      .catch(() => {})
      .finally(() => setLoading(false))
  }, [id])

  if (loading) return <Spinner />
  if (!filme) return (
    <div className="text-center py-24 text-cinema-muted">
      <Film size={48} className="mx-auto mb-4 opacity-30" />
      <p>Filme não encontrado.</p>
    </div>
  )

  const sinopse = filme.sinopse ?? ''
  const shortSinopse = sinopse.length > 220 ? sinopse.slice(0, 220) + '…' : sinopse

  return (
    <div>
      {/* Backdrop hero */}
      <div
        className="relative h-72 md:h-80 overflow-hidden"
        style={{ background: 'linear-gradient(135deg, #0a0a14, #12121e, #1a0c28)' }}
      >
        <div className="absolute inset-0 bg-gradient-to-r from-cinema-base via-cinema-base/70 to-transparent" />
        <div className="absolute inset-0 flex items-end">
          <div className="max-w-7xl mx-auto px-6 pb-8 w-full flex items-end gap-6">
            <div className="hidden md:flex shrink-0 w-32 h-48 rounded-xl items-center justify-center border border-cinema-border"
              style={{ background: 'linear-gradient(135deg, #0a1628, #102040)' }}>
              <Film size={36} className="text-cinema-muted opacity-40" />
            </div>
            <div className="flex-1 min-w-0">
              <button
                onClick={() => navigate(-1)}
                className="flex items-center gap-2 text-cinema-muted hover:text-cinema-primary transition-colors mb-4 text-sm"
              >
                <ArrowLeft size={15} /> Voltar
              </button>
              <div className="flex items-center gap-2 mb-3 flex-wrap">
                <Badge variant="blue">FILME</Badge>
                <Badge variant={classifVariant[filme.classificacao]}>{classifLabel[filme.classificacao]}</Badge>
                <Badge variant="gray">{filme.genero}</Badge>
              </div>
              <h1 className="font-display text-4xl md:text-6xl text-cinema-primary tracking-wide leading-none">
                {filme.titulo}
              </h1>
              <div className="flex items-center gap-4 mt-2 flex-wrap">
                <span className="text-cinema-muted text-sm">{filme.anoLancamento}</span>
                <StarRating value={filme.avaliacao} />
                <span className="text-cinema-muted text-sm">{filme.duracaoMinutos} min</span>
              </div>
            </div>
          </div>
        </div>
      </div>

      {/* Content */}
      <div className="max-w-7xl mx-auto px-6 py-10">
        <div className="grid md:grid-cols-3 gap-8">
          {/* Sinopse */}
          <div className="md:col-span-2">
            {sinopse && (
              <div>
                <h2 className="font-display text-lg tracking-widest text-cinema-primary mb-3">SINOPSE</h2>
                <p className="text-cinema-muted leading-relaxed text-sm">
                  {expanded ? sinopse : shortSinopse}
                  {sinopse.length > 220 && (
                    <button onClick={() => setExpanded(e => !e)} className="ml-2 text-cinema-gold hover:brightness-110 text-xs underline">
                      {expanded ? 'ver menos' : 'ver mais'}
                    </button>
                  )}
                </p>
              </div>
            )}
          </div>

          {/* Details card */}
          <div className="bg-cinema-surface border border-cinema-border rounded-2xl p-5 space-y-4 h-fit">
            <h2 className="font-display text-base tracking-widest text-cinema-primary">DETALHES</h2>
            <Detail icon={User} label="Diretor" value={filme.diretor} />
            <Detail icon={Clock} label="Duração" value={`${filme.duracaoMinutos} minutos`} />
            <Detail icon={Calendar} label="Lançamento" value={String(filme.anoLancamento)} />
            <Detail icon={Star} label="Avaliação" value={`${filme.avaliacao.toFixed(1)} / 10`} gold />
          </div>
        </div>

        {/* Related */}
        {relacionadas.length > 0 && (
          <div className="mt-12">
            <h2 className="font-display text-2xl tracking-widest text-cinema-primary mb-6">OBRAS RELACIONADAS</h2>
            <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-6 gap-4">
              {relacionadas.map(obra => <ObraCard key={obra.id} obra={obra} />)}
            </div>
          </div>
        )}
      </div>
    </div>
  )
}

function Detail({ icon: Icon, label, value, gold }: { icon: typeof User; label: string; value: string; gold?: boolean }) {
  return (
    <div className="flex items-start gap-3 text-sm">
      <Icon size={15} className={`mt-0.5 shrink-0 ${gold ? 'text-cinema-gold' : 'text-cinema-muted'}`} />
      <div>
        <p className="text-cinema-muted text-xs">{label}</p>
        <p className={gold ? 'text-cinema-gold font-semibold' : 'text-cinema-primary'}>{value}</p>
      </div>
    </div>
  )
}
