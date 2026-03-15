import type { Building } from '@/types'
import { BuildingCard } from './BuildingCard'

export function BuildingList({ buildings, onDelete }: { 
  buildings: Building[] 
  onDelete: () => void
}) {
  return (
    <div>
      {buildings.map((building: Building) => (
        <BuildingCard key={building.id} building={building} onDelete={onDelete} />
      ))}
    </div>
  )
}