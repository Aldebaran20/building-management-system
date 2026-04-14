import { authorizedFetch } from '@/utils/authorized-fetch'
import type { SaveWorkOrder } from '@/types'

const API_URL = import.meta.env.VITE_API_URL

export const updateWorkOrder = async (id: number, workOrder: SaveWorkOrder) => {
  const response = await authorizedFetch(`${API_URL}/api/WorkOrders/${id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(workOrder),
  })

  if (!response.ok) {
    throw new Error(`Failed to update work order: ${response.status}`)
  }
}