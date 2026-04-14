import { authorizedFetch } from '@/utils/authorized-fetch'
import type { WorkOrderStatus } from '@/types'

const API_URL = import.meta.env.VITE_API_URL

export const patchWorkOrderStatus = async (id: number, newStatus: WorkOrderStatus) => {
  const response = await authorizedFetch(`${API_URL}/api/WorkOrders/${id}/status`, {
    method: 'PATCH',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(newStatus),
  })

  if (!response.ok) {
    throw new Error(`Failed to update work order status: ${response.status}`)
  }
}