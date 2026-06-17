import { useState } from 'react'
import { Link, NavLink, useNavigate } from 'react-router-dom'
import { Clapperboard, Search, Menu, X, Heart, Settings } from 'lucide-react'
import SearchBar from './SearchBar'

export default function Header() {
  const [menuOpen, setMenuOpen] = useState(false)
  const [searchOpen, setSearchOpen] = useState(false)
  const [searchValue, setSearchValue] = useState('')
  const navigate = useNavigate()

  const handleSearch = (v: string) => {
    setSearchValue(v)
    if (v.trim()) navigate(`/?search=${encodeURIComponent(v.trim())}`)
    else navigate('/')
  }

  const linkClass = ({ isActive }: { isActive: boolean }) =>
    `text-sm font-medium transition-colors py-1 ${
      isActive ? 'text-cinema-gold' : 'text-cinema-muted hover:text-cinema-primary'
    }`

  return (
    <header className="sticky top-0 z-50 bg-cinema-base/85 backdrop-blur-md border-b border-cinema-border">
      <div className="max-w-7xl mx-auto px-6 h-16 flex items-center justify-between gap-4">
        {/* Logo */}
        <Link to="/" className="flex items-center gap-2 shrink-0">
          <Clapperboard size={22} className="text-cinema-gold" />
          <span className="font-display text-xl tracking-widest text-cinema-primary">CINELOG</span>
        </Link>

        {/* Nav desktop */}
        <nav className="hidden md:flex items-center gap-8">
          <NavLink to="/" end className={linkClass}>Catálogo</NavLink>
          <NavLink to="/favoritos" className={linkClass}>
            <span className="flex items-center gap-1.5">
              <Heart size={14} /> Favoritos
            </span>
          </NavLink>
          <NavLink to="/admin" className={linkClass}>
            <span className="flex items-center gap-1.5">
              <Settings size={14} /> Admin
            </span>
          </NavLink>
        </nav>

        {/* Actions */}
        <div className="flex items-center gap-2">
          <button
            onClick={() => { setSearchOpen(s => !s); setSearchValue('') }}
            className="p-2 text-cinema-muted hover:text-cinema-primary transition-colors"
          >
            {searchOpen ? <X size={18} /> : <Search size={18} />}
          </button>
          <button
            onClick={() => setMenuOpen(m => !m)}
            className="md:hidden p-2 text-cinema-muted hover:text-cinema-primary transition-colors"
          >
            {menuOpen ? <X size={18} /> : <Menu size={18} />}
          </button>
        </div>
      </div>

      {/* Search dropdown */}
      {searchOpen && (
        <div className="border-t border-cinema-border py-3 px-6 flex justify-center bg-cinema-base/95">
          <SearchBar
            value={searchValue}
            onChange={handleSearch}
            className="max-w-2xl"
          />
        </div>
      )}

      {/* Mobile menu */}
      {menuOpen && (
        <div className="md:hidden border-t border-cinema-border bg-cinema-surface py-4 px-6 flex flex-col gap-4">
          <NavLink to="/" end className={linkClass} onClick={() => setMenuOpen(false)}>Catálogo</NavLink>
          <NavLink to="/favoritos" className={linkClass} onClick={() => setMenuOpen(false)}>Favoritos</NavLink>
          <NavLink to="/admin" className={linkClass} onClick={() => setMenuOpen(false)}>Admin</NavLink>
        </div>
      )}
    </header>
  )
}
