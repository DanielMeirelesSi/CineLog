import { Search, X } from 'lucide-react'

interface SearchBarProps {
  value: string
  onChange: (value: string) => void
  placeholder?: string
  className?: string
}

export default function SearchBar({ value, onChange, placeholder = 'Buscar filmes e séries...', className = '' }: SearchBarProps) {
  return (
    <div className={`relative w-full ${className}`}>
      <Search size={18} className="absolute left-4 top-1/2 -translate-y-1/2 text-cinema-muted pointer-events-none" />
      <input
        type="text"
        value={value}
        onChange={e => onChange(e.target.value)}
        placeholder={placeholder}
        className="w-full bg-cinema-elevated border border-cinema-border rounded-full pl-11 pr-10 py-3 text-cinema-primary placeholder-cinema-muted focus:outline-none focus:border-cinema-gold transition-colors text-sm"
      />
      {value && (
        <button
          onClick={() => onChange('')}
          className="absolute right-4 top-1/2 -translate-y-1/2 text-cinema-muted hover:text-cinema-primary transition-colors"
        >
          <X size={16} />
        </button>
      )}
    </div>
  )
}
