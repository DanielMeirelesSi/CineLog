import api from './axios'
import type { Avaliacao, CriarAvaliacaoPayload } from '../types'

export const listarAvaliacoesPorObra = (obraId: string) =>
  api.get<Avaliacao[]>(`/avaliacoes/obra/${obraId}`).then(r => r.data)

export const listarAvaliacoesPorUsuario = (usuarioId: string) =>
  api.get<Avaliacao[]>(`/avaliacoes/usuario/${usuarioId}`).then(r => r.data)

export const criarAvaliacao = (
  usuarioId: string,
  obraId: string,
  payload: CriarAvaliacaoPayload
) =>
  api
    .post<Avaliacao>(`/avaliacoes/usuarios/${usuarioId}/obras/${obraId}`, payload)
    .then(r => r.data)