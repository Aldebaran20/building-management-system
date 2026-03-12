export type BuildingType = 'None' | 'Residential' | 'Commercial' | 'Industrial' | 'MixedUse'
export type BuildingStatus = 'None' | 'Active' | 'UnderConstruction'

export type Building = {
  id: number
  buildingName: string
  buildingAddress: string
  numberOfUnits: number,
  buildingType: BuildingType,
  buildingStatus: BuildingStatus,
  dateAdded: string
}

export type SaveBuilding = {
  buildingName: string
  buildingAddress: string
  numberOfUnits: number,
  buildingType: BuildingType,
  buildingStatus: BuildingStatus,
}