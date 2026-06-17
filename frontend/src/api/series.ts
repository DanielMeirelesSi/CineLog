import api from './axios'
import type { Serie, Genero, StatusSerie } from '../types'

export interface SerieDto {
  titulo: string
  genero: Genero
  anoLancamento: number
  sinopse?: string
  avaliacao: number
  numeroTemporadas: number
  numeroEpisodiosPorTemporada: number
  criador: string
  status: StatusSerie
}

export const listarSeries = () =>
  api.get<Serie[]>('/series').then(r => r.data)

export const obterSerie = (id: string) =>
  api.get<Serie>(`/series/${id}`).then(r => r.data)

export const criarSerie = (dto: SerieDto) =>
  api.post<Serie>('/series', dto).then(r => r.data)

export const atualizarSerie = (id: string, dto: SerieDto) =>
  api.put<Serie>(`/series/${id}`, dto).then(r => r.data)

export const deletarSerie = (id: string) =>
  api.delete(`/series/${id}`)
