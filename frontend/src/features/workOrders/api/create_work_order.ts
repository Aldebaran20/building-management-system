import { authorizedFetch } from '@/utils/authorized-fetch'
import type { WorkOrder, SaveWorkOrder } from '@/types'

const API_URL = import.meta.env.VITE_API_URL

export const createWorkOrder = async (workOrder: SaveWorkOrder): Promise<WorkOrder> => {
  const response = await authorizedFetch(`${API_URL}/api/WorkOrders`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(workOrder),
  })

  if (!response.ok) {
    throw new Error(`Failed to create work order: ${response.status}`)
  }

  return await response.json()
}