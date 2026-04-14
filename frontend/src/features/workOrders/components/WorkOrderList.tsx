import type { WorkOrder } from '@/types'
import { WorkOrderCard } from './WorkOrderCard'

export function WorkOrderList({ workOrders, onRefresh, onEditRequest }: { 
  workOrders: WorkOrder[] 
  onRefresh: () => void
  onEditRequest: (workOrder: WorkOrder) => void
}) {
  return (
    <div className="flex flex-col gap-3">
      {workOrders.map((workOrder: WorkOrder) => (
        <WorkOrderCard key={workOrder.id} workOrder={workOrder} onRefresh={onRefresh} onEditRequest={onEditRequest} />
      ))}
    </div>
  )
}