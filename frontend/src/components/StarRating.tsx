import { Star } from 'lucide-react'

interface StarRatingProps {
  value: number
  className?: string
}

export default function StarRating({ value, className = '' }: StarRatingProps) {
  return (
    <div className={`flex items-center gap-1 ${className}`}>
      <Star size={12} className="fill-cinema-gold text-cinema-gold" />
      <span className="text-cinema-gold text-xs font-semibold">{value.toFixed(1)}</span>
    </div>
  )
}
