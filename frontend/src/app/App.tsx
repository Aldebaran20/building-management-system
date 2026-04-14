import { createRouter } from '@tanstack/react-router'
import { rootRoute, authenticatedRoute } from './root-route'
import { loginRoute } from './pages/LoginPage'
import { buildingsRoute } from './pages/BuildingsPage'
import { contractorsRoute } from './pages/ContractorsPage'
import { workOrdersRoute } from './pages/WorkOrdersPage'

const routeTree = rootRoute.addChildren([
    authenticatedRoute.addChildren([buildingsRoute, contractorsRoute, workOrdersRoute]),
    loginRoute
])

export const router = createRouter({ routeTree })
