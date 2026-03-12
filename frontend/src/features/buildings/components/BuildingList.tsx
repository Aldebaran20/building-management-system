import type { Building } from '@/types'
import { BuildingCard } from './BuildingCard'

export function BuildingList({ buildings }: { buildings: Building[] }) {
  return (
    <div className="">
      {buildings.map((building: Building) => (
        <BuildingCard key={building.id} {...building} />
      ))}
    </div>
  )
}