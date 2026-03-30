import { Button } from '@/components/Button'
import { TextInput } from '@/components/TextInput'
import { login } from '../api/login'
import { useNavigate } from '@tanstack/react-router'
import { useState } from 'react'
import type { LoginCredentials } from '@/types'
import type { FormEvent } from 'react'

export function LoginForm() {
  const navigate = useNavigate()
  const [error, setError] = useState<string | null>(null)

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
      event.preventDefault()
      const formData = new FormData(event.currentTarget)
  
      const loginCredentials : LoginCredentials = {
        email: String(formData.get('userEmail') ?? ''),
        password: String(formData.get('userPassword') ?? '')
      } 
  
      login(loginCredentials)
        .then((token) => {
          setError(null)
          sessionStorage.setItem('token', token)
          navigate({ to: '/' })
        })
        .catch((error) => {
          console.error(`Login failed: ${error}`)
          setError('Invalid email or password')
        })
    }
  
  return (
    <form className="flex flex-col gap-8" onSubmit={handleSubmit}>
      <TextInput label="Email" type="email" name="userEmail" placeholder="demo@bms.com"/>
      <TextInput label="Password" type="password" name="userPassword" placeholder="password123"/>
      <div className="flex flex-col gap-2">
        {error && <p className="text-sm text-red-400">{error}</p>}
        <Button type="submit" className="self-end">Login</Button>
      </div>
    </form>
  )
}