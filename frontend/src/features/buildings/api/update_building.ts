import type { SaveBuilding } from "@/types"

const API_URL = import.meta.env.VITE_API_URL

export const updateBuilding = async (id: number, building: SaveBuilding) => {
  const response = await fetch(`${API_URL}/api/Buildings/${id}`, {
    method: 'PUT',
    headers: {
      'accept': 'application/json',
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(building),
  })

  if (!response.ok) {
    throw new Error(`Failed to update building: ${response.status}`)
  }
}