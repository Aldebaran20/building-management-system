import type { WorkOrder, WorkOrderStatus } from '@/types'
import { WORK_ORDER_STATUSES } from '@/types'
import { formatPascalCase } from '@/utils/format-pascal-case'
import { useState } from 'react'
import { ActionsDropdown } from '@/components/ActionsDropdown'
import { ConfirmDeleteModal } from '@/components/ConfirmDeleteModal'
import { deleteWorkOrder } from '../api/delete_work_order'
import { patchWorkOrderStatus } from '../api/patch_work_order_status'
import { getContractorDisplayName } from '../utils/get-contractor-display-name'
import { SelectInput } from '@/components/SelectInput'

export function WorkOrderCard({ workOrder, onRefresh, onEditRequest }: {
  workOrder: WorkOrder
  onRefresh: () => void
  onEditRequest: (workOrder: WorkOrder) => void
}) {
  const { title, description, priority, status, dateCreated, deadline, building, contractor } = workOrder
  const [isDropdownOpen, setIsDropdownOpen] = useState(false)
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false)

  const handleDelete = () => {
    deleteWorkOrder(workOrder.id)
      .then(onRefresh)
      .catch((error) => {
        console.error('Error deleting work order:', error)
      })
  }

  const handleStatusChange = (newStatus: WorkOrderStatus) => {
    patchWorkOrderStatus(workOrder.id, newStatus)
      .then(onRefresh)
      .catch((error) => {
        console.error('Error updating work order status:', error)
        onRefresh() // Refresh to revert to old status if update fails
      })
  }

  return (
    <div className="flex border border-zinc-800 group rounded-md p-4 h-40 items-center bg-zinc-900 hover:bg-zinc-800 transition-colors duration-150">
      <HoverPopover
        trigger={<InfoField label="Title" value={title} />}
      >
        <InfoField label="Description" value={description || 'No description provided.'} />
      </HoverPopover>
      <HoverPopover 
        trigger={<InfoField label="Building" value={building.buildingName} />}
      > 
        <InfoField label="Address" value={building.buildingAddress} />
        <InfoField label="Type" value={formatPascalCase(building.buildingType)} />
      </HoverPopover>
      <HoverPopover
        trigger={<InfoField label="Contractor" value={getContractorDisplayName(contractor)} />}
      >
        <InfoField label="Business Name" value={contractor.businessName} />
        <InfoField label="Contact Name" value={contractor.contactName} />
        <InfoField label="Phone" value={contractor.contactPhone} />
        <InfoField label="Email" value={contractor.contactEmail} />
        <InfoField label="Type" value={formatPascalCase(contractor.contractorType)} />
      </HoverPopover>
      <div className="flex-1 text-center">
        <SelectInput 
          label="Status"
          labelSize="xs"
          name="status"
          options={WORK_ORDER_STATUSES}
          defaultValue={status}
          onChange={(e) => handleStatusChange(e.target.value as WorkOrderStatus)}
        />
      </div>
      <div className="flex-1 text-center">
        <InfoField label="Priority" value={formatPascalCase(priority || '-')} />
      </div>
      <div className="flex-1 text-center flex flex-col gap-3">
        <InfoField label="Date Created" value={dateCreated} />
        {deadline && (
          <InfoField label="Deadline" value={deadline} />
        )}
      </div>
      <div className="relative">
        <button 
          className="flex-none w-8 text-center cursor-pointer" 
          onClick={() => setIsDropdownOpen(!isDropdownOpen)}
          aria-label={`Open actions menu for ${title}`}
        >
          ...
        </button>
        {isDropdownOpen && (
          <ActionsDropdown 
            onMouseLeave={() => setIsDropdownOpen(false)}
            onEditRequest={() => onEditRequest(workOrder)}
            onDelete={() => setIsDeleteModalOpen(true)}
          />
        )}
      </div>
      {isDeleteModalOpen && (
        <ConfirmDeleteModal 
          onConfirm={() => {setIsDeleteModalOpen(false); handleDelete()}} 
          onCancel={() => setIsDeleteModalOpen(false)} 
          entityName="this work order"
        />
      )}
    </div>
  )
}

function HoverPopover({ trigger, children }: { trigger: React.ReactNode; children: React.ReactNode }) {
  const [isOpen, setIsOpen] = useState(false)
  return (
    <div className="flex-2 text-center relative"
      onMouseOver={() => setIsOpen(true)}
      onMouseLeave={() => setIsOpen(false)}
    >
      {trigger}
      {isOpen && (
        <div className="flex flex-col gap-2 absolute left-1/2 -translate-x-1/2 z-10 border border-zinc-600 bg-zinc-900 text-white text-sm p-2 rounded-md mt-2 w-64">
          {children}
        </div>
      )}
    </div>
  )
}

function InfoField({ label, value }: { label: string; value: string | undefined }) {
  if (!value) return (null)
  return (
    <div>
      <div className="text-xs text-zinc-500 mb-1">{label}</div>
      <div>{value}</div>
    </div>
  )
}