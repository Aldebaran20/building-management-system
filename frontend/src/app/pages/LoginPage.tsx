import { LoginForm } from '@/features/auth/components/LoginForm'
import { createRoute } from '@tanstack/react-router'
import { rootRoute } from '../App'

export const loginRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/login',
  component: LoginPage,
})

function LoginPage() {
  return (
    <div className="flex justify-center items-center min-h-screen bg-zinc-950 text-white">
      <div className="w-100 flex flex-col gap-8 p-8 bg-zinc-900 border border-zinc-800 rounded-xl">
        <div>
          <h1 className="text-2xl font-semibold mb-1">Login</h1>
          <p className="text-sm text-zinc-400">Please sign in to your account</p>
        </div>
        <LoginForm />
      </div>
    </div>
  )
}