// Buildings

// Type for possible values returned by the backend
export type BuildingType = 'Residential' | 'Commercial' | 'Industrial' | 'MixedUse'
export type BuildingStatus = 'Active' | 'UnderConstruction'

// Array for user-selectable options in form dropdowns
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

// Contractors

// Type for possible values returned by the backend
export type ContractorType = 'Electrical' | 'Plumbing' | 'Carpentry' | 'Painting' | 'Roofing' | 'HVAC' | 'Landscaping' | 'Cleaning' | 'General'
export type ContractorStatus = 'Available' | 'Unavailable' | 'Inactive'

// Array for user-selectable options in form dropdowns
export const CONTRACTOR_TYPES: ContractorType[] = ['Electrical', 'Plumbing', 'Carpentry', 'Painting', 'Roofing', 'HVAC', 'Landscaping', 'Cleaning', 'General']
export const CONTRACTOR_STATUSES: ContractorStatus[] = ['Available', 'Unavailable', 'Inactive']

export type Contractor = {
  id: number
  businessName?: string
  contactName?: string
  contactEmail?: string
  contactPhone: string
  areaOfOperation?: string
  contractorType: ContractorType
  contractorStatus: ContractorStatus
  dateAdded: string
}

export type SaveContractor = {
  businessName?: string
  contactName?: string
  contactEmail?: string
  contactPhone: string
  areaOfOperation?: string
  contractorType: ContractorType
  contractorStatus: ContractorStatus
}

// Auth

export type LoginCredentials = {
  email: string
  password: string
}