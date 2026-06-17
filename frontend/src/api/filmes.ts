import api from './axios'
import type { Filme, Genero, ClassificacaoEtaria } from '../types'

export interface FilmeDto {
  titulo: string
  genero: Genero
  anoLancamento: number
  sinopse?: string
  avaliacao: number
  duracaoMinutos: number
  diretor: string
  classificacao: ClassificacaoEtaria
}

export const listarFilmes = () =>
  api.get<Filme[]>('/filmes').then(r => r.data)

export const obterFilme = (id: string) =>
  api.get<Filme>(`/filmes/${id}`).then(r => r.data)

export const criarFilme = (dto: FilmeDto) =>
  api.post<Filme>('/filmes', dto).then(r => r.data)

export const atualizarFilme = (id: string, dto: FilmeDto) =>
  api.put<Filme>(`/filmes/${id}`, dto).then(r => r.data)

export const deletarFilme = (id: string) =>
  api.delete(`/filmes/${id}`)
