import { authorizedFetch } from '@/utils/authorized-fetch'
const API_URL = import.meta.env.VITE_API_URL

export const deleteContractor = async (id: number) => {
  const response = await authorizedFetch(`${API_URL}/api/Contractors/${id}`, {
    method: 'DELETE',
  })

  if (!response.ok) {
    throw new Error(`Failed to delete contractor: ${response.status}`)
  }
}