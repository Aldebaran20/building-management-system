export const authorizedFetch = (url: string, options: RequestInit = {}): Promise<Response> => {
  const token = sessionStorage.getItem('token')
  
  if (!token) {
    throw new Error('No authentication token found')
  }

  return fetch(url, {
    ...options,
    headers: {
      'accept': 'application/json',
      'Authorization': `Bearer ${token}`,
      ...options.headers,
    },
  })
}