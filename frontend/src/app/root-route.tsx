import { createRoute, createRootRoute, Outlet } from '@tanstack/react-router'
import { Navbar } from '@/components/Navbar'

export const rootRoute = createRootRoute({ component: () => <Outlet /> })

export const authenticatedRoute = createRoute({
  getParentRoute: () => rootRoute,
  id: 'authenticated',
  component: () => (
    <div className="flex h-full">
      <Navbar />
      <main className="flex-1 overflow-y-auto">
        <Outlet />
      </main>
    </div>
  )
})
