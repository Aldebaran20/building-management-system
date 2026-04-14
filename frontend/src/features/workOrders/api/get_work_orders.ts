import { authorizedFetch } from '@/utils/authorized-fetch'
import type { WorkOrder } from '@/types'

const API_URL = import.meta.env.VITE_API_URL

export const getWorkOrders = async (): Promise<WorkOrder[]> => {
    const response = await authorizedFetch(`${API_URL}/api/WorkOrders`, {
    method: 'GET',
  })

    if (!response.ok) {
        throw new Error(`Failed to fetch work orders: ${response.status}`)
    }

    return await response.json()
}