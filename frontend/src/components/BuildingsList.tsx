import type { Building } from '../types'
import { BuildingRow } from './BuildingRow'

export function BuildingsList({ buildings }: { buildings: Building[] }) {
  return (
    <div className="mt-20">
      {buildings.map((building: Building) => (
        <BuildingRow key={building.id} {...building} />
      ))}
    </div>
  )
}