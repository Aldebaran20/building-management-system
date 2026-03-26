// Type for possible values returned by the backend, including 'None' (unset)
export type BuildingType = 'None' | 'Residential' | 'Commercial' | 'Industrial' | 'MixedUse'
export type BuildingStatus = 'None' | 'Active' | 'UnderConstruction'

// Array for user-selectable options in form dropdowns, 'None' excluded
export const BUILDING_TYPES: BuildingType[] = ['Residential', 'Commercial', 'Industrial', 'MixedUse']
export const BUILDING_STATUSES: BuildingStatus[] = ['Active', 'UnderConstruction']

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

export type LoginCredentials = {
  email: string
  password: string
}