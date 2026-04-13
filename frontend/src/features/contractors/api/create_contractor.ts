import { authorizedFetch } from '@/utils/authorized-fetch'
import type { Contractor, SaveContractor } from '@/types'

const API_URL = import.meta.env.VITE_API_URL

export const createContractor = async (contractor: SaveContractor): Promise<Contractor> => {
  const response = await authorizedFetch(`${API_URL}/api/Contractors`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(contractor),
  })

  if (!response.ok) {
    throw new Error(`Failed to create contractor: ${response.status}`)
  }

  return await response.json()
}