import { useState, useEffect } from 'react'
import { Heart } from 'lucide-react'
import { listarUsuarios, obterFavoritos, removerFavorito } from '../api/usuarios'
import type { Usuario, ObraResumo } from '../types'
import ObraCard from '../components/ObraCard'
import Spinner from '../components/ui/Spinner'
import EmptyState from '../components/ui/EmptyState'
import { useToast } from '../contexts/ToastContext'

export default function Favoritos() {
  const [usuarios, setUsuarios] = useState<Usuario[]>([])
  const [usuarioId, setUsuarioId] = useState('')
  const [favoritos, setFavoritos] = useState<ObraResumo[]>([])
  const [loading, setLoading] = useState(false)
  const { showToast } = useToast()

  useEffect(() => {
    listarUsuarios()
      .then(lista => {
        setUsuarios(lista)

        const usuarioSalvo = localStorage.getItem('cinelog_usuario_ativo')
        const usuarioExiste = lista.some(u => u.id === usuarioSalvo)

        if (usuarioSalvo && usuarioExiste) {
          setUsuarioId(usuarioSalvo)
        } else if (lista.length > 0) {
          setUsuarioId(lista[0].id)
          localStorage.setItem('cinelog_usuario_ativo', lista[0].id)
        }
      })
      .catch(() => {
        setUsuarios([])
        setUsuarioId('')
      })
  }, [])

  useEffect(() => {
    if (!usuarioId) {
      setFavoritos([])
      return
    }

    localStorage.setItem('cinelog_usuario_ativo', usuarioId)

    setLoading(true)

    obterFavoritos(usuarioId)
      .then(setFavoritos)
      .catch(() => {
        setFavoritos([])
        showToast('Erro ao carregar favoritos.', 'error')
      })
      .finally(() => setLoading(false))
  }, [usuarioId, showToast])

  const handleTrocarUsuario = (novoUsuarioId: string) => {
    setUsuarioId(novoUsuarioId)
    localStorage.setItem('cinelog_usuario_ativo', novoUsuarioId)
  }

  const handleRemover = async (obraId: string) => {
    if (!usuarioId) {
      showToast('Selecione um usuário antes de remover favoritos.', 'error')
      return
    }

    try {
      await removerFavorito(usuarioId, obraId)
      setFavoritos(prev => prev.filter(f => f.id !== obraId))
      showToast('Removido dos favoritos.', 'info')
    } catch {
      showToast('Erro ao remover favorito.', 'error')
    }
  }

  const usuario = usuarios.find(u => u.id === usuarioId)

  return (
    <div className="max-w-7xl mx-auto px-6 py-10">
      {/* Header */}
      <div className="flex items-center gap-4 mb-8 flex-wrap">
        <h1 className="font-display text-3xl tracking-widest text-cinema-primary flex items-center gap-2">
          <Heart size={22} className="text-cinema-red fill-cinema-red" />
          FAVORITOS
        </h1>

        {usuarios.length > 0 ? (
          <select
            value={usuarioId}
            onChange={e => handleTrocarUsuario(e.target.value)}
            className="bg-cinema-elevated border border-cinema-border text-cinema-primary rounded-lg px-4 py-2 text-sm focus:outline-none focus:border-cinema-gold transition-colors"
          >
            {usuarios.map(u => (
              <option key={u.id} value={u.id}>
                {u.nome}
              </option>
            ))}
          </select>
        ) : (
          <span className="text-cinema-muted text-sm">
            Nenhum usuário cadastrado.
          </span>
        )}

        {usuario && (
          <span className="text-cinema-muted text-sm ml-auto">
            Usuário: <span className="text-cinema-gold">{usuario.nome}</span> ·{' '}
            {favoritos.length} favorito{favoritos.length !== 1 ? 's' : ''}
          </span>
        )}
      </div>

      {usuarios.length === 0 ? (
        <EmptyState
          icon={Heart}
          title="NENHUM USUÁRIO CADASTRADO"
          description="Cadastre um usuário na aba Admin para usar a lista de favoritos."
        />
      ) : loading ? (
        <Spinner />
      ) : favoritos.length === 0 ? (
        <EmptyState
          icon={Heart}
          title="NENHUM FAVORITO"
          description="Explore o catálogo e clique no coração para adicionar obras aos favoritos."
        />
      ) : (
        <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 gap-4">
          {favoritos.map(obra => (
            <ObraCard
              key={obra.id}
              obra={obra}
              isFavorito
              onToggleFavorito={handleRemover}
            />
          ))}
        </div>
      )}
    </div>
  )
}