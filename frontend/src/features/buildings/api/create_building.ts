import { authorizedFetch } from '@/utils/authorized-fetch'
import type { Building, SaveBuilding } from '@/types'

const API_URL = import.meta.env.VITE_API_URL

export const createBuilding = async (building: SaveBuilding): Promise<Building> => {
  const response = await authorizedFetch(`${API_URL}/api/Buildings`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(building),
  })

  if (!response.ok) {
    throw new Error(`Failed to create building: ${response.status}`)
  }

  return await response.json()
}