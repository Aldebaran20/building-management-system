import { createRootRoute, createRouter, Outlet } from '@tanstack/react-router'
import { loginRoute } from './pages/LoginPage'
import { buildingsRoute } from './pages/BuildingsPage'

const RootLayout = () => <Outlet />

export const rootRoute = createRootRoute({ component: RootLayout })

const routeTree = rootRoute.addChildren([loginRoute, buildingsRoute])

export const router = createRouter({ routeTree })
