import { CheckCircle, XCircle, Info } from 'lucide-react'
import { useToast } from '../../contexts/ToastContext'
import type { ToastType } from '../../contexts/ToastContext'

const icons: Record<ToastType, typeof CheckCircle> = {
  success: CheckCircle,
  error: XCircle,
  info: Info,
}

const styles: Record<ToastType, string> = {
  success: 'border-green-600/40 bg-green-950/90 text-green-300',
  error:   'border-red-600/40 bg-red-950/90 text-red-300',
  info:    'border-blue-600/40 bg-blue-950/90 text-blue-300',
}

export default function ToastContainer() {
  const { toasts } = useToast()
  return (
    <div className="fixed bottom-6 right-6 z-[100] flex flex-col gap-2 pointer-events-none">
      {toasts.map(toast => {
        const Icon = icons[toast.type]
        return (
          <div
            key={toast.id}
            className={`flex items-center gap-3 px-4 py-3 rounded-xl border backdrop-blur-md shadow-2xl text-sm font-medium pointer-events-auto ${styles[toast.type]}`}
          >
            <Icon size={16} />
            {toast.message}
          </div>
        )
      })}
    </div>
  )
}
