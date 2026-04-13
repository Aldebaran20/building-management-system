import { createRouter } from '@tanstack/react-router'
import { rootRoute, authenticatedRoute } from './root-route'
import { loginRoute } from './pages/LoginPage'
import { buildingsRoute } from './pages/BuildingsPage'
import { contractorsRoute } from './pages/ContractorsPage'

const routeTree = rootRoute.addChildren([
    authenticatedRoute.addChildren([buildingsRoute, contractorsRoute]),
    loginRoute
])

export const router = createRouter({ routeTree })
