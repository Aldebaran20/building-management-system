const API_URL = import.meta.env.VITE_API_URL

export const deleteBuilding = async (id: number) => {
  const response = await fetch(`${API_URL}/api/Buildings/${id}`, {
    method: 'DELETE',
    headers: {
      'accept': 'application/json',
      'Authorization': `Bearer ${sessionStorage.getItem('token')}`
    },
  })

  if (!response.ok) {
    throw new Error(`Failed to delete building: ${response.status}`)
  }
}