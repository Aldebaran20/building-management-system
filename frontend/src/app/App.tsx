import { createRouter } from '@tanstack/react-router'
import { rootRoute } from './root-route'
import { loginRoute } from './pages/LoginPage'
import { buildingsRoute } from './pages/BuildingsPage'

const routeTree = rootRoute.addChildren([loginRoute, buildingsRoute])

export const router = createRouter({ routeTree })
