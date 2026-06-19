import { useState } from 'react'
import type { ObraResumo, Usuario, Avaliacao } from '../types'
import { criarAvaliacao } from '../api/avaliacoes'
import Modal from './ui/Modal'
import { useToast } from '../contexts/ToastContext'

interface AvaliacaoModalProps {
  isOpen: boolean
  obra: ObraResumo | null
  usuario: Usuario | null
  onClose: () => void
  onAvaliacaoCriada: (avaliacao: Avaliacao) => void
}

export default function AvaliacaoModal({
  isOpen,
  obra,
  usuario,
  onClose,
  onAvaliacaoCriada,
}: AvaliacaoModalProps) {
  const [nota, setNota] = useState('10')
  const [comentario, setComentario] = useState('')
  const [salvando, setSalvando] = useState(false)
  const { showToast } = useToast()

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()

    if (!obra) {
      showToast('Selecione uma obra para avaliar.', 'error')
      return
    }

    if (!usuario) {
      showToast('Selecione um usuário antes de avaliar.', 'error')
      return
    }

    const notaNumerica = Number(nota)

    if (Number.isNaN(notaNumerica) || notaNumerica < 0 || notaNumerica > 10) {
      showToast('A nota deve estar entre 0 e 10.', 'error')
      return
    }

    try {
      setSalvando(true)

      const avaliacao = await criarAvaliacao(usuario.id, obra.id, {
        nota: notaNumerica,
        comentario: comentario.trim() || undefined,
      })

      showToast('Avaliação cadastrada com sucesso.', 'success')
      onAvaliacaoCriada(avaliacao)
      setNota('10')
      setComentario('')
      onClose()
    } catch (error: any) {
      const mensagem =
        error?.response?.data?.erro ??
        error?.response?.data?.message ??
        'Erro ao cadastrar avaliação.'

      showToast(mensagem, 'error')
    } finally {
      setSalvando(false)
    }
  }

  return (
    <Modal
      isOpen={isOpen}
      onClose={onClose}
      title={obra ? `Avaliar ${obra.titulo}` : 'Avaliar obra'}
    >
      <form onSubmit={handleSubmit} className="space-y-4">
        {usuario ? (
          <p className="text-cinema-muted text-sm">
            Avaliando como:{' '}
            <span className="text-cinema-gold font-semibold">{usuario.nome}</span>
          </p>
        ) : (
          <p className="text-cinema-red text-sm">
            Cadastre ou selecione um usuário antes de avaliar.
          </p>
        )}

        <div>
          <label className="block text-cinema-muted text-xs uppercase tracking-wider mb-2">
            Nota
          </label>
          <input
            type="number"
            min="0"
            max="10"
            step="0.1"
            value={nota}
            onChange={e => setNota(e.target.value)}
            className="w-full bg-cinema-elevated border border-cinema-border rounded-lg px-4 py-2 text-cinema-primary focus:outline-none focus:border-cinema-gold"
          />
        </div>

        <div>
          <label className="block text-cinema-muted text-xs uppercase tracking-wider mb-2">
            Comentário
          </label>
          <textarea
            value={comentario}
            onChange={e => setComentario(e.target.value)}
            maxLength={1000}
            rows={4}
            placeholder="Escreva um comentário opcional..."
            className="w-full bg-cinema-elevated border border-cinema-border rounded-lg px-4 py-2 text-cinema-primary resize-none focus:outline-none focus:border-cinema-gold"
          />
        </div>

        <button
          type="submit"
          disabled={salvando || !usuario}
          className="w-full bg-cinema-gold text-black font-semibold rounded-lg px-4 py-2 hover:brightness-110 disabled:opacity-50 disabled:cursor-not-allowed transition-all"
        >
          {salvando ? 'Salvando...' : 'Salvar avaliação'}
        </button>
      </form>
    </Modal>
  )
}