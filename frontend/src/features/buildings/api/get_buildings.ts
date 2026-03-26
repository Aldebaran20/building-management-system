import type { Building } from '@/types'

const API_URL = import.meta.env.VITE_API_URL

export const getBuildings = async (): Promise<Building[]> => {
    const response = await fetch(`${API_URL}/api/Buildings`, {
    method: 'GET',
    headers: {
      'accept': 'application/json',
      'Authorization': `Bearer ${sessionStorage.getItem('token')}`
    }
  })

    if (!response.ok) {
        throw new Error(`Failed to fetch buildings: ${response.status}`)
    }

    return await response.json()
}