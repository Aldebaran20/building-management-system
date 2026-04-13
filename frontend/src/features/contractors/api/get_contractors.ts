import { authorizedFetch } from '@/utils/authorized-fetch'
import type { Contractor } from '@/types'

const API_URL = import.meta.env.VITE_API_URL

export const getContractors = async (): Promise<Contractor[]> => {
    const response = await authorizedFetch(`${API_URL}/api/Contractors`, {
    method: 'GET',
  })

    if (!response.ok) {
        throw new Error(`Failed to fetch contractors: ${response.status}`)
    }

    return await response.json()
}