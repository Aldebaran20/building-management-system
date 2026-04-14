import { useEffect, useState } from 'react'
import type { WorkOrder, Building, Contractor } from '@/types'
import { getWorkOrders } from '@/features/workOrders/api/get_work_orders'
import { getBuildings } from '@/features/buildings/api/get_buildings'
import { getContractors } from '@/features/contractors/api/get_contractors'
import { WorkOrderForm } from '@/features/workOrders/components/WorkOrderForm'
import { WorkOrderList } from '@/features/workOrders/components/WorkOrderList'
import { Button } from '@/components/Button'
import { createRoute } from '@tanstack/react-router'
import { authenticatedRoute } from '@/app/root-route'

export const workOrdersRoute = createRoute({
  getParentRoute: () => authenticatedRoute,
  path: '/work-orders',
  component: WorkOrdersPage,
})

function WorkOrdersPage() {
  const [workOrders, setWorkOrders] = useState<WorkOrder[]>([])
  const [isWorkOrderFormVisible, setIsWorkOrderFormVisible] = useState(false)
  const [editingWorkOrder, setEditingWorkOrder] = useState<WorkOrder | null>(null)
  const [buildings, setBuildings] = useState<Building[]>([])
  const [contractors, setContractors] = useState<Contractor[]>([])

  const fetchWorkOrders = () => {
    getWorkOrders()
      .then((data) => setWorkOrders(data))
      .catch((error) => {
        console.error(`Error fetching work orders: ${error}`)
      })
  }

  useEffect(() => {
    fetchWorkOrders()
    getBuildings().then(setBuildings)
      .catch((error) => {
        console.error(`Error fetching buildings: ${error}`)
      })
    getContractors().then(setContractors)
      .catch((error) => {
        console.error(`Error fetching contractors: ${error}`)
      })
  }, [])

  const openAddForm = () => {
    setEditingWorkOrder(null)
    setIsWorkOrderFormVisible(true)
  }
  const openEditForm = (workOrder: WorkOrder) => {
    setEditingWorkOrder(workOrder)
    setIsWorkOrderFormVisible(true)
  }
  const closeForm = () => {
    setIsWorkOrderFormVisible(false)
    setEditingWorkOrder(null)
  }

  const renderHeader = () => {
    if (!isWorkOrderFormVisible) {
      return (
        <>
          <div>
            <h1 className="text-2xl font-semibold mb-1">Work Orders</h1>
            <p className="text-sm text-zinc-400">Manage your work orders here</p>
          </div>
          <Button onClick={openAddForm}>Add Work Order</Button>
        </>
      );
    }

    return (
      <>
        <div>
          <h1 className="text-2xl font-semibold mb-1">
            {editingWorkOrder ? 'Edit Work Order' : 'New Work Order'}
          </h1>
          <p className="text-sm text-zinc-400">
            {editingWorkOrder ? 'Edit the work order details below' : 'Add a new work order'}
          </p>
        </div>
        <Button variant="danger" onClick={closeForm}>Cancel</Button>
      </>
    );
  }

  const renderContent = () => {
    if (isWorkOrderFormVisible) {
      return (
        <WorkOrderForm 
          onSuccess={() => { closeForm(); fetchWorkOrders(); }}
          workOrder={editingWorkOrder ?? undefined}
          buildings={buildings} contractors={contractors}
        />
      );
    }

    if (workOrders.length === 0) {
      return (
        <div className="text-center flex flex-col gap-2 mt-52">
          <h1 className="text-4xl font-semibold">No work orders yet</h1>
          <p className="text-md text-zinc-400">Add your first work order to get started</p>
        </div>
      );
    }

    return (
      <WorkOrderList 
        workOrders={workOrders} 
        onRefresh={() => fetchWorkOrders()} 
        onEditRequest={openEditForm}
      />
    );
  }

  return (
    <div className="py-8 px-10 bg-zinc-950 text-white flex flex-col gap-14">
      <div className="flex items-center justify-between">
        {renderHeader()}
      </div>
      {renderContent()}
    </div>
  )
}