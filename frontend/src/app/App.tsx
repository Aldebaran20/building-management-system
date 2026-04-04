import { createRouter } from '@tanstack/react-router'
import { rootRoute, authenticatedRoute } from './root-route'
import { loginRoute } from './pages/LoginPage'
import { buildingsRoute } from './pages/BuildingsPage'

const routeTree = rootRoute.addChildren([
    authenticatedRoute.addChildren([buildingsRoute]),
    loginRoute
])

export const router = createRouter({ routeTree })
