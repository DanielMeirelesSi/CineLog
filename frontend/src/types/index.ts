export type Genero =
  | 'Acao' | 'Comedia' | 'Drama' | 'Terror' | 'Romance' | 'SciFi'
  | 'Documentario' | 'Animacao' | 'Thriller' | 'Aventura' | 'Fantasia' | 'Crime'

export type StatusSerie = 'EmAndamento' | 'Finalizada' | 'Cancelada' | 'Pausada'

export type ClassificacaoEtaria = 'Livre' | 'Dez' | 'Doze' | 'Quatorze' | 'Dezesseis' | 'Dezoito'

export type TipoObra = 'Filme' | 'Serie'

export interface ObraResumo {
  id: string
  titulo: string
  genero: Genero
  anoLancamento: number
  avaliacao: number
  tipo: TipoObra
  detalhes: string
}

export interface Filme {
  id: string
  titulo: string
  genero: Genero
  anoLancamento: number
  sinopse?: string
  avaliacao: number
  duracaoMinutos: number
  diretor: string
  classificacao: ClassificacaoEtaria
  createdAt: string
  updatedAt: string
}

export interface Serie {
  id: string
  titulo: string
  genero: Genero
  anoLancamento: number
  sinopse?: string
  avaliacao: number
  numeroTemporadas: number
  numeroEpisodiosPorTemporada: number
  criador: string
  status: StatusSerie
  createdAt: string
  updatedAt: string
}

export interface Usuario {
  id: string
  nome: string
  email: string
  totalFavoritos: number
  createdAt: string
}
