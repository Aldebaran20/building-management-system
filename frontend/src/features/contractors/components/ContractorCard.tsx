import type { Contractor } from '@/types'
import { formatPascalCase } from '@/utils/format-pascal-case'
import { useState } from 'react'
import { ActionsDropdown } from '@/components/ActionsDropdown'
import { ConfirmDeleteModal } from '@/components/ConfirmDeleteModal'
import { deleteContractor } from '../api/delete_contractor'

export function ContractorCard({ contractor, onDelete, onEditRequest }: {
  contractor: Contractor
  onDelete: () => void
  onEditRequest: (contractor: Contractor) => void
}) {
  const { businessName, contactName, contactEmail, contactPhone, areaOfOperation, contractorType, contractorStatus, dateAdded } = contractor
  const [isDropdownOpen, setIsDropdownOpen] = useState(false)
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false)

  const handleDelete = () => {
    deleteContractor(contractor.id)
      .then(() => {
        onDelete()
      })
      .catch((error) => {
        console.error('Error deleting contractor:', error)
      })
  }

  return (
    <div className="flex border border-zinc-800 rounded-md p-4 h-40 items-center gap-4 bg-zinc-900 hover:bg-zinc-800 transition-colors duration-150">
      <div className="h-32 w-20 flex-none bg-zinc-500 rounded-md" />
      <div className="flex-2">
        <div>{contactName}</div>
        <div className="text-sm text-zinc-400">{businessName}</div>
      </div>
      <div className="flex-2 text-center flex flex-col gap-3">
        {contactEmail && (
          <div className="flex-2 text-center">
            <div className="text-xs text-zinc-500 mb-1">Email</div>
            <div>{formatPascalCase(contactEmail)}</div>
          </div>
        )}
        <div className="flex-2 text-center">
          <div className="text-xs text-zinc-500 mb-1">Phone Number</div>
          <div>{formatPascalCase(contactPhone)}</div>
        </div>
      </div>
      <div className="flex-2 text-center">
        <div className="text-xs text-zinc-500 mb-1">Type</div>
        <div>{formatPascalCase(contractorType)}</div>
      </div>
      <div className="flex-2 text-center">
        <div className="text-xs text-zinc-500 mb-1">Status</div>
        <div>{formatPascalCase(contractorStatus)}</div>
      </div>
      <div className="flex-1 text-center">
        <div className="text-xs text-zinc-500 mb-1">Area of Operation</div>
        <div>{areaOfOperation || '-'}</div>
      </div>
      <div className="flex-2 text-center">
        <div className="text-xs text-zinc-500 mb-1">Date Added</div>
        <div>{dateAdded}</div>
      </div>
      <div className="relative">
        <button 
          className="flex-none w-8 text-center cursor-pointer" 
          onClick={() => setIsDropdownOpen(!isDropdownOpen)}
          aria-label={`Open actions menu for ${contactName}`}
        >
          ...
        </button>
        {isDropdownOpen && (
          <ActionsDropdown 
            onMouseLeave={() => setIsDropdownOpen(false)}
            onEditRequest={() => onEditRequest(contractor)}
            onDelete={() => setIsDeleteModalOpen(true)}
          />
        )}
      </div>
      {isDeleteModalOpen && (
        <ConfirmDeleteModal 
          onConfirm={() => {setIsDeleteModalOpen(false); handleDelete()}} 
          onCancel={() => setIsDeleteModalOpen(false)} 
          entityName="this contractor"
        />
      )}
    </div>
  )
}