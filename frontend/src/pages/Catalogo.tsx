import { useState, useEffect } from 'react'
import { useSearchParams } from 'react-router-dom'
import { Film } from 'lucide-react'
import { listarCatalogo, buscarPorTitulo } from '../api/catalogo'
import { useDebounce } from '../hooks/useDebounce'
import type { ObraResumo, Genero } from '../types'
import ObraCard from '../components/ObraCard'
import SearchBar from '../components/SearchBar'
import FilterChips from '../components/FilterChips'
import SkeletonCard from '../components/SkeletonCard'
import EmptyState from '../components/ui/EmptyState'

export default function Catalogo() {
  const [searchParams, setSearchParams] = useSearchParams()
  const [search, setSearch] = useState(searchParams.get('search') ?? '')
  const [tipo, setTipo] = useState('')
  const [genero, setGenero] = useState<Genero | ''>('')
  const [obras, setObras] = useState<ObraResumo[]>([])
  const [loading, setLoading] = useState(true)
  const debouncedSearch = useDebounce(search, 300)

  useEffect(() => {
    const q = searchParams.get('search')
    if (q) setSearch(q)
  }, [searchParams])

  useEffect(() => {
    setLoading(true)
    const req = debouncedSearch.trim()
      ? buscarPorTitulo(debouncedSearch)
      : listarCatalogo(tipo || undefined, genero || undefined)
    req
      .then(setObras)
      .catch(() => setObras([]))
      .finally(() => setLoading(false))
  }, [debouncedSearch, tipo, genero])

  const handleSearch = (v: string) => {
    setSearch(v)
    if (v.trim()) setSearchParams({ search: v })
    else setSearchParams({})
  }

  const handleTipo = (t: string) => { setTipo(t); setSearch(''); setSearchParams({}) }
  const handleGenero = (g: Genero | '') => { setGenero(g); setSearch(''); setSearchParams({}) }
  const handleClear = () => { setSearch(''); setTipo(''); setGenero(''); setSearchParams({}) }

  return (
    <div>
      {/* Hero */}
      <div className="relative overflow-hidden py-20 px-6" style={{ background: 'linear-gradient(135deg, #08080F 0%, #12121E 55%, #1a0a1e 100%)' }}>
        <div className="absolute top-1/3 left-1/3 w-96 h-96 rounded-full blur-3xl pointer-events-none" style={{ background: 'rgba(232,184,75,0.04)' }} />
        <div className="absolute bottom-0 right-1/4 w-72 h-72 rounded-full blur-3xl pointer-events-none" style={{ background: 'rgba(100,50,150,0.06)' }} />
        <div className="relative max-w-4xl mx-auto text-center">
          <p className="text-cinema-muted text-sm tracking-[0.3em] uppercase mb-4 font-medium">Catálogo Audiovisual</p>
          <h1 className="font-display text-5xl md:text-7xl text-cinema-primary tracking-widest mb-3 leading-none">
            DESCUBRA SUA<br />
            <span className="text-cinema-gold">PRÓXIMA OBRA</span>
          </h1>
          <p className="text-cinema-muted mb-10 text-base">
            Filmes e séries para todos os gostos — explore, filtre e favorite
          </p>
          <div className="flex justify-center">
            <SearchBar value={search} onChange={handleSearch} className="max-w-xl" />
          </div>
        </div>
      </div>

      {/* Filter bar */}
      <div className="sticky top-16 z-30 bg-cinema-base/90 backdrop-blur-sm border-b border-cinema-border">
        <div className="max-w-7xl mx-auto px-6 py-4">
          <FilterChips tipo={tipo} genero={genero} onTipoChange={handleTipo} onGeneroChange={handleGenero} />
        </div>
      </div>

      {/* Grid */}
      <div className="max-w-7xl mx-auto px-6 py-8">
        {loading ? (
          <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 gap-4">
            {Array.from({ length: 10 }).map((_, i) => <SkeletonCard key={i} />)}
          </div>
        ) : obras.length === 0 ? (
          <EmptyState
            icon={Film}
            title="NENHUMA OBRA ENCONTRADA"
            description="Tente buscar por outro título ou remova os filtros aplicados."
            action={{ label: 'Limpar filtros', onClick: handleClear }}
          />
        ) : (
          <>
            <p className="text-cinema-muted text-xs mb-4">{obras.length} obra{obras.length !== 1 ? 's' : ''} encontrada{obras.length !== 1 ? 's' : ''}</p>
            <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 gap-4">
              {obras.map(obra => <ObraCard key={obra.id} obra={obra} />)}
            </div>
          </>
        )}
      </div>
    </div>
  )
}
