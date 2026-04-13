import { authorizedFetch } from '@/utils/authorized-fetch'
import type { SaveContractor } from '@/types'

const API_URL = import.meta.env.VITE_API_URL

export const updateContractor = async (id: number, contractor: SaveContractor) => {
  const response = await authorizedFetch(`${API_URL}/api/Contractors/${id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(contractor),
  })

  if (!response.ok) {
    throw new Error(`Failed to update contractor: ${response.status}`)
  }
}