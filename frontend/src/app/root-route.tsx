import { createRoute, createRootRoute, Outlet, redirect } from '@tanstack/react-router'
import { Navbar } from '@/components/Navbar'

export const rootRoute = createRootRoute({ component: () => <Outlet /> })

export const authenticatedRoute = createRoute({
  getParentRoute: () => rootRoute,
  id: 'authenticated',
  beforeLoad: () => {
    if (!sessionStorage.getItem('token')) {
      throw redirect({ to: '/login' })
    }
  },
  component: () => (
    <div className="flex h-full">
      <Navbar />
      <main className="flex-1 overflow-y-auto">
        <Outlet />
      </main>
    </div>
  )
})
