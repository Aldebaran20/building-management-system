import type { Building } from '../types'

export function BuildingRow({
  buildingName,
  buildingAddress,
  numberOfUnits,
  buildingType,
  buildingStatus,
  dateAdded 
}: Building) {

  return (
    <div className="flex">
      <div className="flex-4 text-left">{buildingName}</div>
      <div className="flex-5 text-left">{buildingAddress}</div>
      <div className="flex-1 text-left">{numberOfUnits || '750'}</div>
      <div className="flex-3 text-left">{buildingType || 'Residential'}</div>
      <div className="flex-3 text-left">{buildingStatus || 'Under Construction'}</div>
      <div className="flex-2 text-left">{dateAdded || '09/12/2025'}</div>
    </div>
  )
}