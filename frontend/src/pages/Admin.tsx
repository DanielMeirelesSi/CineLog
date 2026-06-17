import { useState, useEffect } from 'react'
import { Plus, Pencil, Trash2, Film, Tv, Users } from 'lucide-react'
import type { Filme, Serie, Usuario, Genero, ClassificacaoEtaria, StatusSerie } from '../types'
import { listarFilmes, criarFilme, atualizarFilme, deletarFilme, type FilmeDto } from '../api/filmes'
import { listarSeries, criarSerie, atualizarSerie, deletarSerie, type SerieDto } from '../api/series'
import { listarUsuarios, criarUsuario, deletarUsuario } from '../api/usuarios'
import Drawer from '../components/ui/Drawer'
import Modal from '../components/ui/Modal'
import Spinner from '../components/ui/Spinner'
import { useToast } from '../contexts/ToastContext'
import { GENERO_LABELS } from '../components/FilterChips'

type Tab = 'filmes' | 'series' | 'usuarios'

const GENEROS: Genero[] = ['Acao','Comedia','Drama','Terror','Romance','SciFi','Documentario','Animacao','Thriller','Aventura','Fantasia','Crime']
const CLASSIFICACOES: ClassificacaoEtaria[] = ['Livre','Dez','Doze','Quatorze','Dezesseis','Dezoito']
const STATUSES: StatusSerie[] = ['EmAndamento','Finalizada','Cancelada','Pausada']
const CLASSIF_LABELS: Record<ClassificacaoEtaria, string> = { Livre:'Livre', Dez:'10+', Doze:'12+', Quatorze:'14+', Dezesseis:'16+', Dezoito:'18+' }
const STATUS_LABELS: Record<StatusSerie, string> = { EmAndamento:'Em andamento', Finalizada:'Finalizada', Cancelada:'Cancelada', Pausada:'Pausada' }

const EMPTY_FILME: FilmeDto = { titulo:'', genero:'Acao', anoLancamento: new Date().getFullYear(), sinopse:'', avaliacao:7.0, duracaoMinutos:90, diretor:'', classificacao:'Livre' }
const EMPTY_SERIE: SerieDto = { titulo:'', genero:'Drama', anoLancamento: new Date().getFullYear(), sinopse:'', avaliacao:7.0, numeroTemporadas:1, numeroEpisodiosPorTemporada:10, criador:'', status:'EmAndamento' }

const inputCls = 'w-full bg-cinema-elevated border border-cinema-border rounded-lg px-3 py-2 text-cinema-primary placeholder-cinema-muted focus:outline-none focus:border-cinema-gold transition-colors text-sm'
const labelCls = 'block text-cinema-muted text-xs mb-1 font-medium'

export default function Admin() {
  const [tab, setTab] = useState<Tab>('filmes')
  const [filmes, setFilmes] = useState<Filme[]>([])
  const [series, setSeries] = useState<Serie[]>([])
  const [usuarios, setUsuarios] = useState<Usuario[]>([])
  const [loading, setLoading] = useState(true)
  const [drawerOpen, setDrawerOpen] = useState(false)
  const [editingFilme, setEditingFilme] = useState<Filme | null>(null)
  const [editingSerie, setEditingSerie] = useState<Serie | null>(null)
  const [filmeForm, setFilmeForm] = useState<FilmeDto>(EMPTY_FILME)
  const [serieForm, setSerieForm] = useState<SerieDto>(EMPTY_SERIE)
  const [usuarioForm, setUsuarioForm] = useState({ nome: '', email: '' })
  const [deleteModal, setDeleteModal] = useState<{ id: string; tipo: Tab } | null>(null)
  const { showToast } = useToast()

  const load = () => {
    setLoading(true)
    Promise.all([listarFilmes(), listarSeries(), listarUsuarios()])
      .then(([f, s, u]) => { setFilmes(f); setSeries(s); setUsuarios(u) })
      .catch(() => showToast('Erro ao carregar dados', 'error'))
      .finally(() => setLoading(false))
  }

  useEffect(() => { load() }, [])

  const openNewFilme = () => { setEditingFilme(null); setFilmeForm(EMPTY_FILME); setDrawerOpen(true) }
  const openEditFilme = (f: Filme) => {
    setEditingFilme(f)
    setFilmeForm({ titulo: f.titulo, genero: f.genero, anoLancamento: f.anoLancamento, sinopse: f.sinopse ?? '', avaliacao: f.avaliacao, duracaoMinutos: f.duracaoMinutos, diretor: f.diretor, classificacao: f.classificacao })
    setDrawerOpen(true)
  }
  const openNewSerie = () => { setEditingSerie(null); setSerieForm(EMPTY_SERIE); setDrawerOpen(true) }
  const openEditSerie = (s: Serie) => {
    setEditingSerie(s)
    setSerieForm({ titulo: s.titulo, genero: s.genero, anoLancamento: s.anoLancamento, sinopse: s.sinopse ?? '', avaliacao: s.avaliacao, numeroTemporadas: s.numeroTemporadas, numeroEpisodiosPorTemporada: s.numeroEpisodiosPorTemporada, criador: s.criador, status: s.status })
    setDrawerOpen(true)
  }

  const handleSaveFilme = async () => {
    try {
      if (editingFilme) await atualizarFilme(editingFilme.id, filmeForm)
      else await criarFilme(filmeForm)
      showToast(editingFilme ? 'Filme atualizado!' : 'Filme criado!', 'success')
      setDrawerOpen(false); load()
    } catch { showToast('Erro ao salvar filme', 'error') }
  }

  const handleSaveSerie = async () => {
    try {
      if (editingSerie) await atualizarSerie(editingSerie.id, serieForm)
      else await criarSerie(serieForm)
      showToast(editingSerie ? 'Série atualizada!' : 'Série criada!', 'success')
      setDrawerOpen(false); load()
    } catch { showToast('Erro ao salvar série', 'error') }
  }

  const handleSaveUsuario = async () => {
    try {
      await criarUsuario(usuarioForm.nome, usuarioForm.email)
      showToast('Usuário criado!', 'success')
      setDrawerOpen(false); setUsuarioForm({ nome: '', email: '' }); load()
    } catch { showToast('Erro ao criar usuário', 'error') }
  }

  const handleDelete = async () => {
    if (!deleteModal) return
    try {
      if (deleteModal.tipo === 'filmes') await deletarFilme(deleteModal.id)
      else if (deleteModal.tipo === 'series') await deletarSerie(deleteModal.id)
      else await deletarUsuario(deleteModal.id)
      showToast('Removido com sucesso', 'info')
      setDeleteModal(null); load()
    } catch { showToast('Erro ao remover', 'error') }
  }

  const tabCls = (t: Tab) =>
    `flex items-center gap-2 px-5 py-3 text-sm font-medium border-b-2 transition-colors ${
      tab === t ? 'border-cinema-gold text-cinema-gold' : 'border-transparent text-cinema-muted hover:text-cinema-primary'
    }`

  const drawerTitle =
    tab === 'filmes' ? (editingFilme ? 'Editar Filme' : 'Novo Filme') :
    tab === 'series' ? (editingSerie ? 'Editar Série' : 'Nova Série') : 'Novo Usuário'

  return (
    <div className="max-w-7xl mx-auto px-6 py-8">
      <h1 className="font-display text-3xl tracking-widest text-cinema-primary mb-6">ADMIN</h1>

      {/* Tabs */}
      <div className="flex border-b border-cinema-border mb-6">
        <button className={tabCls('filmes')} onClick={() => setTab('filmes')}><Film size={15} /> Filmes ({filmes.length})</button>
        <button className={tabCls('series')} onClick={() => setTab('series')}><Tv size={15} /> Séries ({series.length})</button>
        <button className={tabCls('usuarios')} onClick={() => setTab('usuarios')}><Users size={15} /> Usuários ({usuarios.length})</button>
      </div>

      {/* Action bar */}
      <div className="flex justify-between items-center mb-5">
        <p className="text-cinema-muted text-sm">
          {tab === 'filmes' ? `${filmes.length} filmes` : tab === 'series' ? `${series.length} séries` : `${usuarios.length} usuários`}
        </p>
        <button
          onClick={() => { tab === 'filmes' ? openNewFilme() : tab === 'series' ? openNewSerie() : (() => { setDrawerOpen(true); setUsuarioForm({ nome: '', email: '' }) })() }}
          className="flex items-center gap-2 bg-cinema-gold text-black px-4 py-2 rounded-lg text-sm font-semibold hover:brightness-110 transition-all"
        >
          <Plus size={15} /> {tab === 'filmes' ? 'Novo Filme' : tab === 'series' ? 'Nova Série' : 'Novo Usuário'}
        </button>
      </div>

      {loading ? <Spinner /> : (
        <div className="bg-cinema-surface border border-cinema-border rounded-2xl overflow-hidden">
          <table className="w-full text-sm">
            <thead>
              <tr className="border-b border-cinema-border text-cinema-muted text-xs uppercase tracking-wider">
                <th className="text-left px-5 py-3 font-medium">Título / Nome</th>
                <th className="text-left px-5 py-3 font-medium hidden md:table-cell">
                  {tab === 'usuarios' ? 'E-mail' : 'Gênero'}
                </th>
                <th className="text-left px-5 py-3 font-medium hidden md:table-cell">
                  {tab === 'filmes' ? 'Duração' : tab === 'series' ? 'Temporadas' : 'Favoritos'}
                </th>
                <th className="text-left px-5 py-3 font-medium">Avaliação</th>
                <th className="px-5 py-3 w-20">Ações</th>
              </tr>
            </thead>
            <tbody>
              {tab === 'filmes' && filmes.map(f => (
                <tr key={f.id} className="border-b border-cinema-border/50 hover:bg-cinema-elevated/40 transition-colors">
                  <td className="px-5 py-3 text-cinema-primary font-medium">{f.titulo}</td>
                  <td className="px-5 py-3 text-cinema-muted hidden md:table-cell">{GENERO_LABELS[f.genero]}</td>
                  <td className="px-5 py-3 text-cinema-muted hidden md:table-cell">{f.duracaoMinutos} min</td>
                  <td className="px-5 py-3 text-cinema-gold font-semibold">★ {f.avaliacao.toFixed(1)}</td>
                  <td className="px-5 py-3">
                    <Actions onEdit={() => openEditFilme(f)} onDelete={() => setDeleteModal({ id: f.id, tipo: 'filmes' })} />
                  </td>
                </tr>
              ))}
              {tab === 'series' && series.map(s => (
                <tr key={s.id} className="border-b border-cinema-border/50 hover:bg-cinema-elevated/40 transition-colors">
                  <td className="px-5 py-3 text-cinema-primary font-medium">{s.titulo}</td>
                  <td className="px-5 py-3 text-cinema-muted hidden md:table-cell">{GENERO_LABELS[s.genero]}</td>
                  <td className="px-5 py-3 text-cinema-muted hidden md:table-cell">{s.numeroTemporadas} temp.</td>
                  <td className="px-5 py-3 text-cinema-gold font-semibold">★ {s.avaliacao.toFixed(1)}</td>
                  <td className="px-5 py-3">
                    <Actions onEdit={() => openEditSerie(s)} onDelete={() => setDeleteModal({ id: s.id, tipo: 'series' })} />
                  </td>
                </tr>
              ))}
              {tab === 'usuarios' && usuarios.map(u => (
                <tr key={u.id} className="border-b border-cinema-border/50 hover:bg-cinema-elevated/40 transition-colors">
                  <td className="px-5 py-3 text-cinema-primary font-medium">{u.nome}</td>
                  <td className="px-5 py-3 text-cinema-muted hidden md:table-cell">{u.email}</td>
                  <td className="px-5 py-3 text-cinema-muted hidden md:table-cell">{u.totalFavoritos}</td>
                  <td className="px-5 py-3 text-cinema-muted">—</td>
                  <td className="px-5 py-3">
                    <Actions onDelete={() => setDeleteModal({ id: u.id, tipo: 'usuarios' })} />
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      {/* Drawer */}
      <Drawer isOpen={drawerOpen} onClose={() => setDrawerOpen(false)} title={drawerTitle}>
        {tab === 'filmes' && (
          <FilmeForm form={filmeForm} onChange={setFilmeForm} onSave={handleSaveFilme} onCancel={() => setDrawerOpen(false)} />
        )}
        {tab === 'series' && (
          <SerieFormComponent form={serieForm} onChange={setSerieForm} onSave={handleSaveSerie} onCancel={() => setDrawerOpen(false)} />
        )}
        {tab === 'usuarios' && (
          <div className="flex flex-col gap-4">
            <div>
              <label className={labelCls}>Nome *</label>
              <input className={inputCls} value={usuarioForm.nome} onChange={e => setUsuarioForm(f => ({ ...f, nome: e.target.value }))} placeholder="Nome completo" />
            </div>
            <div>
              <label className={labelCls}>E-mail *</label>
              <input className={inputCls} type="email" value={usuarioForm.email} onChange={e => setUsuarioForm(f => ({ ...f, email: e.target.value }))} placeholder="email@exemplo.com" />
            </div>
            <FormActions onSave={handleSaveUsuario} onCancel={() => setDrawerOpen(false)} />
          </div>
        )}
      </Drawer>

      {/* Delete confirmation */}
      <Modal isOpen={!!deleteModal} onClose={() => setDeleteModal(null)} title="Confirmar exclusão">
        <p className="text-cinema-muted text-sm mb-6">Esta ação não pode ser desfeita. Deseja continuar?</p>
        <div className="flex gap-3 justify-end">
          <button onClick={() => setDeleteModal(null)} className="px-4 py-2 rounded-lg border border-cinema-border text-cinema-muted hover:text-cinema-primary text-sm transition-colors">Cancelar</button>
          <button onClick={handleDelete} className="px-4 py-2 rounded-lg bg-cinema-red text-white text-sm font-semibold hover:brightness-110 transition-all">Excluir</button>
        </div>
      </Modal>
    </div>
  )
}

function Actions({ onEdit, onDelete }: { onEdit?: () => void; onDelete: () => void }) {
  return (
    <div className="flex items-center gap-1">
      {onEdit && (
        <button onClick={onEdit} className="p-1.5 text-cinema-muted hover:text-cinema-gold transition-colors rounded">
          <Pencil size={14} />
        </button>
      )}
      <button onClick={onDelete} className="p-1.5 text-cinema-muted hover:text-cinema-red transition-colors rounded">
        <Trash2 size={14} />
      </button>
    </div>
  )
}

function FilmeForm({ form, onChange, onSave, onCancel }: { form: FilmeDto; onChange: (f: FilmeDto) => void; onSave: () => void; onCancel: () => void }) {
  const set = (key: keyof FilmeDto, val: unknown) => onChange({ ...form, [key]: val })
  return (
    <div className="flex flex-col gap-4">
      <div>
        <label className={labelCls}>Título *</label>
        <input className={inputCls} value={form.titulo} onChange={e => set('titulo', e.target.value)} placeholder="Título do filme" />
      </div>
      <div className="grid grid-cols-2 gap-3">
        <div>
          <label className={labelCls}>Gênero *</label>
          <select className={inputCls} value={form.genero} onChange={e => set('genero', e.target.value as Genero)}>
            {GENEROS.map(g => <option key={g} value={g}>{GENERO_LABELS[g]}</option>)}
          </select>
        </div>
        <div>
          <label className={labelCls}>Ano *</label>
          <input className={inputCls} type="number" value={form.anoLancamento} onChange={e => set('anoLancamento', Number(e.target.value))} />
        </div>
      </div>
      <div className="grid grid-cols-2 gap-3">
        <div>
          <label className={labelCls}>Avaliação (0–10) *</label>
          <input className={inputCls} type="number" step="0.1" min="0" max="10" value={form.avaliacao} onChange={e => set('avaliacao', Number(e.target.value))} />
        </div>
        <div>
          <label className={labelCls}>Duração (min) *</label>
          <input className={inputCls} type="number" value={form.duracaoMinutos} onChange={e => set('duracaoMinutos', Number(e.target.value))} />
        </div>
      </div>
      <div>
        <label className={labelCls}>Diretor *</label>
        <input className={inputCls} value={form.diretor} onChange={e => set('diretor', e.target.value)} placeholder="Nome do diretor" />
      </div>
      <div>
        <label className={labelCls}>Classificação *</label>
        <select className={inputCls} value={form.classificacao} onChange={e => set('classificacao', e.target.value as ClassificacaoEtaria)}>
          {CLASSIFICACOES.map(c => <option key={c} value={c}>{CLASSIF_LABELS[c]}</option>)}
        </select>
      </div>
      <div>
        <label className={labelCls}>Sinopse</label>
        <textarea className={`${inputCls} resize-none`} rows={3} value={form.sinopse} onChange={e => set('sinopse', e.target.value)} placeholder="Descrição do filme..." />
      </div>
      <FormActions onSave={onSave} onCancel={onCancel} />
    </div>
  )
}

function SerieFormComponent({ form, onChange, onSave, onCancel }: { form: SerieDto; onChange: (f: SerieDto) => void; onSave: () => void; onCancel: () => void }) {
  const set = (key: keyof SerieDto, val: unknown) => onChange({ ...form, [key]: val })
  return (
    <div className="flex flex-col gap-4">
      <div>
        <label className={labelCls}>Título *</label>
        <input className={inputCls} value={form.titulo} onChange={e => set('titulo', e.target.value)} placeholder="Título da série" />
      </div>
      <div className="grid grid-cols-2 gap-3">
        <div>
          <label className={labelCls}>Gênero *</label>
          <select className={inputCls} value={form.genero} onChange={e => set('genero', e.target.value as Genero)}>
            {GENEROS.map(g => <option key={g} value={g}>{GENERO_LABELS[g]}</option>)}
          </select>
        </div>
        <div>
          <label className={labelCls}>Ano *</label>
          <input className={inputCls} type="number" value={form.anoLancamento} onChange={e => set('anoLancamento', Number(e.target.value))} />
        </div>
      </div>
      <div className="grid grid-cols-2 gap-3">
        <div>
          <label className={labelCls}>Avaliação (0–10) *</label>
          <input className={inputCls} type="number" step="0.1" min="0" max="10" value={form.avaliacao} onChange={e => set('avaliacao', Number(e.target.value))} />
        </div>
        <div>
          <label className={labelCls}>Temporadas *</label>
          <input className={inputCls} type="number" min="1" value={form.numeroTemporadas} onChange={e => set('numeroTemporadas', Number(e.target.value))} />
        </div>
      </div>
      <div className="grid grid-cols-2 gap-3">
        <div>
          <label className={labelCls}>Ep. por temporada *</label>
          <input className={inputCls} type="number" min="1" value={form.numeroEpisodiosPorTemporada} onChange={e => set('numeroEpisodiosPorTemporada', Number(e.target.value))} />
        </div>
        <div>
          <label className={labelCls}>Status *</label>
          <select className={inputCls} value={form.status} onChange={e => set('status', e.target.value as StatusSerie)}>
            {STATUSES.map(s => <option key={s} value={s}>{STATUS_LABELS[s]}</option>)}
          </select>
        </div>
      </div>
      <div>
        <label className={labelCls}>Criador *</label>
        <input className={inputCls} value={form.criador} onChange={e => set('criador', e.target.value)} placeholder="Nome do criador" />
      </div>
      <div>
        <label className={labelCls}>Sinopse</label>
        <textarea className={`${inputCls} resize-none`} rows={3} value={form.sinopse} onChange={e => set('sinopse', e.target.value)} placeholder="Descrição da série..." />
      </div>
      <FormActions onSave={onSave} onCancel={onCancel} />
    </div>
  )
}

function FormActions({ onSave, onCancel }: { onSave: () => void; onCancel: () => void }) {
  return (
    <div className="flex gap-3 justify-end pt-2">
      <button onClick={onCancel} className="px-4 py-2 rounded-lg border border-cinema-border text-cinema-muted hover:text-cinema-primary text-sm transition-colors">Cancelar</button>
      <button onClick={onSave} className="px-4 py-2 rounded-lg bg-cinema-gold text-black text-sm font-semibold hover:brightness-110 transition-all">Salvar</button>
    </div>
  )
}
