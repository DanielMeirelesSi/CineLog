import api from './axios'
import type { Usuario, ObraResumo } from '../types'

export const listarUsuarios = () =>
  api.get<Usuario[]>('/usuarios').then(r => r.data)

export const criarUsuario = (nome: string, email: string) =>
  api.post<Usuario>('/usuarios', { nome, email }).then(r => r.data)

export const atualizarUsuario = (id: string, nome: string, email: string) =>
  api.put<Usuario>(`/usuarios/${id}`, { nome, email }).then(r => r.data)

export const deletarUsuario = (id: string) =>
  api.delete(`/usuarios/${id}`)

export const obterFavoritos = (usuarioId: string) =>
  api.get<ObraResumo[]>(`/usuarios/${usuarioId}/favoritos`).then(r => r.data)

export const adicionarFavorito = (usuarioId: string, obraId: string) =>
  api.post(`/usuarios/${usuarioId}/favoritos/${obraId}`)

export const removerFavorito = (usuarioId: string, obraId: string) =>
  api.delete(`/usuarios/${usuarioId}/favoritos/${obraId}`)
