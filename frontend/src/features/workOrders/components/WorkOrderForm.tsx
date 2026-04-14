import type { WorkOrder, SaveWorkOrder, WorkOrderContractor, WorkOrderBuilding, WorkOrderPriority, Building, Contractor } from '@/types'
import { WORK_ORDER_PRIORITIES } from '@/types'
import { createWorkOrder } from '../api/create_work_order'
import { updateWorkOrder } from '../api/update_work_order'
import { SelectInput } from '@/components/SelectInput'
import { TextInput } from '@/components/TextInput'
import { Button } from '@/components/Button'
import { getContractorDisplayName } from '@/features/workOrders/utils/get-contractor-display-name'
import type { FormEvent } from 'react'

export function WorkOrderForm({ onSuccess, workOrder, buildings, contractors }: {
  onSuccess: () => void
  workOrder?: WorkOrder
  buildings: Building[]
  contractors: Contractor[]
}) {
  // Set default values for the form fields based on whether we're editing an existing work order or creating a new one
  const {
    title = '',
    description = '',
    priority = '',
    deadline = '',
    building = { id: 0, buildingName: '' } as WorkOrderBuilding,
    contractor = { id: 0, businessName: undefined, contactName: undefined, contactPhone: '' } as WorkOrderContractor,
  } = workOrder ?? {}

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    const formData = new FormData(event.currentTarget)

    const savedWorkOrder : SaveWorkOrder = {
      buildingId: Number(formData.get('building')),
      contractorId: Number(formData.get('contractor')),
      title: String(formData.get('title') ?? ''),
      description: formData.get('description') as string || undefined,
      priority: formData.get('priority') as WorkOrderPriority || undefined,
      deadline: formData.get('deadline') as string || undefined,
    } 

    if (workOrder) {
      updateWorkOrder(workOrder.id, savedWorkOrder)
        .then(() => {
          onSuccess()
        })
        .catch((error) => {
          console.error(`Error updating work order with ID ${workOrder.id}:`, error)
        })
    } else {
      createWorkOrder(savedWorkOrder)
        .then(() => {
          onSuccess()
        })
        .catch((error) => {
          console.error(`Error creating work order:`, error)
        })
    }
  }

  return (
    <form className="flex flex-col gap-8" onSubmit={handleSubmit}>
      <TextInput label="Title" type="text" name="title" required defaultValue={title}/>
      <TextInput label="Description" type="text" name="description" defaultValue={description}/>
      <div className="grid grid-cols-2 gap-4">
        <SelectInput label="Building" name="building" required 
          options={buildings.map((b : Building) => ({ value: b.id, label: b.buildingName }))} 
          defaultValue={building.id}
        />
        <SelectInput label="Contractor" name="contractor" required
          options={contractors.map((c : Contractor) => ({ value: c.id, label: getContractorDisplayName(c) }))} 
          defaultValue={contractor.id}
        />
      </div>
      <div className="grid grid-cols-2 gap-4">
        <SelectInput label="Priority Level" name="priority" options={WORK_ORDER_PRIORITIES} defaultValue={priority}/>
        <TextInput label="Deadline" type="date" name="deadline" defaultValue={deadline}/>
      </div>
      <Button type="submit" className="self-end">{workOrder ? "Update Work Order" : "Add Work Order"}</Button>
    </form>
  )
}