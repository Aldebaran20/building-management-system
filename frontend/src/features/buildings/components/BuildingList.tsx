import type { Building } from '@/types'
import { BuildingCard } from './BuildingCard'

export function BuildingList({ buildings }: { buildings: Building[] }) {
  return (
    <div className="">
      {buildings.map((building: Building) => (
        <BuildingCard key={building.id} {...building} />
      ))}
      <BuildingCard key="1" id={0} 
        buildingName="New Building" 
        buildingAddress="123 Main St, Parramatta, NSW 2150"
        numberOfUnits={5}
        buildingStatus="UnderConstruction"
        buildingType="Residential"
        dateAdded="02-03-2025"
      />
      <BuildingCard key="2" id={1} 
        buildingName="Fresh Building"
        buildingAddress="245 Berkeley St"
        numberOfUnits={5}
        buildingStatus="UnderConstruction"
        buildingType="Residential"
        dateAdded="02-03-2025"
      />
    </div>
  )
}