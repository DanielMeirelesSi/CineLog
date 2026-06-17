export default function SkeletonCard() {
  return (
    <div className="rounded-xl overflow-hidden bg-cinema-surface border border-cinema-border animate-pulse">
      <div className="aspect-[2/3] bg-cinema-elevated" />
      <div className="p-3 space-y-2">
        <div className="h-5 bg-cinema-elevated rounded w-4/5" />
        <div className="h-3 bg-cinema-elevated rounded w-1/2" />
        <div className="h-3 bg-cinema-elevated rounded w-1/4" />
      </div>
    </div>
  )
}
