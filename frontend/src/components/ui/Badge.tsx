import type { ReactNode } from 'react'

type BadgeVariant = 'gold' | 'red' | 'blue' | 'purple' | 'green' | 'orange' | 'gray'

interface BadgeProps {
  children: ReactNode
  variant?: BadgeVariant
}

const variants: Record<BadgeVariant, string> = {
  gold:   'bg-yellow-900/40 text-yellow-300 border-yellow-700/40',
  red:    'bg-red-900/40 text-red-400 border-red-800/40',
  blue:   'bg-blue-900/40 text-blue-400 border-blue-800/40',
  purple: 'bg-purple-900/40 text-purple-400 border-purple-800/40',
  green:  'bg-green-900/40 text-green-400 border-green-800/40',
  orange: 'bg-orange-900/40 text-orange-400 border-orange-800/40',
  gray:   'bg-zinc-800/60 text-zinc-400 border-zinc-700/40',
}

export default function Badge({ children, variant = 'gray' }: BadgeProps) {
  return (
    <span className={`inline-flex items-center px-2 py-0.5 rounded text-xs font-semibold border tracking-wide ${variants[variant]}`}>
      {children}
    </span>
  )
}
