import type { LoginCredentials } from '@/types'

const API_URL = import.meta.env.VITE_API_URL

export const login = async (loginCredentials: LoginCredentials): Promise<string> => {
  const response = await fetch(`${API_URL}/api/Auth/login`, {
    method: 'POST',
    headers: {
      'accept': 'application/json',
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(loginCredentials),
  })

  if (!response.ok) {
    throw new Error(`Failed to login: ${response.status}`)
  }

  var data = await response.json()
  return data.token
}