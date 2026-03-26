import type { Building } from '@/types'
import { BuildingCard } from './BuildingCard'

export function BuildingList({ buildings, onDelete, onEditRequest }: { 
  buildings: Building[] 
  onDelete: () => void
  onEditRequest: (building: Building) => void
}) {
  return (
    <div className="flex flex-col gap-3">
      {buildings.map((building: Building) => (
        <BuildingCard key={building.id} building={building} onDelete={onDelete} onEditRequest={onEditRequest} />
      ))}
    </div>
  )
}