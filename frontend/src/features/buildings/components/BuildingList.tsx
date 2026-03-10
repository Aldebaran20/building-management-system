import type { Building } from '../../../types'
import { BuildingRow } from './BuildingRow'

export function BuildingList({ buildings }: { buildings: Building[] }) {
  return (
    <div className="">
      {buildings.map((building: Building) => (
        <BuildingRow key={building.id} {...building} />
      ))}
      <BuildingRow key="1" id={0} buildingName="New Building" buildingAddress="123 Main St, Parramatta, NSW 2150" />
      <BuildingRow key="2" id={1} buildingName="Fresh Building" buildingAddress="245 Berkeley St" />
      <BuildingRow key="3" id={2} buildingName="Noir Building" buildingAddress="368 Camper St" />
      <BuildingRow key="4" id={3} buildingName="Old Building" buildingAddress="689 Denning St" />
    </div>
  )
}