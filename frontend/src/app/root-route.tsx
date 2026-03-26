import { createRootRoute, Outlet } from '@tanstack/react-router'

const RootLayout = () => <Outlet />
export const rootRoute = createRootRoute({ component: RootLayout })
