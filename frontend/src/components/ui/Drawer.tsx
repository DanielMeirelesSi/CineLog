import { X } from 'lucide-react'
import type { ReactNode } from 'react'

interface DrawerProps {
  isOpen: boolean
  onClose: () => void
  title: string
  children: ReactNode
}

export default function Drawer({ isOpen, onClose, title, children }: DrawerProps) {
  return (
    <>
      {isOpen && (
        <div className="fixed inset-0 z-40 bg-black/50 backdrop-blur-sm" onClick={onClose} />
      )}
      <div
        className={`fixed inset-y-0 right-0 z-50 w-full max-w-[500px] bg-cinema-surface border-l border-cinema-border shadow-2xl transform transition-transform duration-300 ease-out ${
          isOpen ? 'translate-x-0' : 'translate-x-full'
        }`}
      >
        <div className="flex items-center justify-between p-6 border-b border-cinema-border">
          <h2 className="font-display text-xl tracking-wide text-cinema-primary">{title}</h2>
          <button onClick={onClose} className="text-cinema-muted hover:text-cinema-primary transition-colors">
            <X size={20} />
          </button>
        </div>
        <div className="p-6 overflow-y-auto h-[calc(100%-5rem)]">{children}</div>
      </div>
    </>
  )
}
