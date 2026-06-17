import api from './axios'
import type { ObraResumo, Genero } from '../types'

export const listarCatalogo = (tipo?: string, genero?: Genero) =>
  api.get<ObraResumo[]>('/catalogo', { params: { tipo: tipo || undefined, genero: genero || undefined } }).then(r => r.data)

export const buscarPorTitulo = (titulo: string) =>
  api.get<ObraResumo[]>('/catalogo/buscar', { params: { titulo } }).then(r => r.data)

export const filtrarPorGenero = (genero: Genero) =>
  api.get<ObraResumo[]>(`/catalogo/genero/${genero}`).then(r => r.data)
