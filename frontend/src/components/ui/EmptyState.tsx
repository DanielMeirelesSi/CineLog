import type { LucideIcon } from 'lucide-react'

interface EmptyStateProps {
  icon: LucideIcon
  title: string
  description: string
  action?: { label: string; onClick: () => void }
}

export default function EmptyState({ icon: Icon, title, description, action }: EmptyStateProps) {
  return (
    <div className="flex flex-col items-center justify-center py-24 text-center">
      <Icon size={52} className="text-cinema-muted mb-5 opacity-40" />
      <h3 className="font-display text-2xl tracking-widest text-cinema-primary mb-2">{title}</h3>
      <p className="text-cinema-muted text-sm max-w-xs leading-relaxed">{description}</p>
      {action && (
        <button
          onClick={action.onClick}
          className="mt-6 bg-cinema-gold text-black px-6 py-2 rounded-full font-semibold text-sm hover:brightness-110 transition-all"
        >
          {action.label}
        </button>
      )}
    </div>
  )
}
