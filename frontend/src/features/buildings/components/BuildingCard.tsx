import type { Building } from '@/types'
import { formatPascalCase } from '@/utils/format-pascal-case'
import { useState } from 'react'
import { ActionsDropdown } from '@/components/ActionsDropdown'
import { ConfirmDeleteModal } from '@/components/ConfirmDeleteModal'
import { deleteBuilding } from '../api/delete_building'

export function BuildingCard({ building, onDelete, onEditRequest }: {
  building: Building
  onDelete: () => void
  onEditRequest: (building: Building) => void
}) {
  const { buildingName, buildingAddress, numberOfUnits, buildingType, buildingStatus, dateAdded } = building
  const [isDropdownOpen, setIsDropdownOpen] = useState(false)
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false)

  const handleDelete = () => {
    deleteBuilding(building.id)
      .then(() => {
        onDelete()
      })
      .catch((error) => {
        console.error("Error deleting building:", error)
      })
  }

  return (
    <div className="flex border border-zinc-800 rounded-md p-4 h-40 items-center hover:shadow-2xl mb-3 gap-4 bg-zinc-900 hover:bg-zinc-800 transition-colors duration-150">
      <div 
        className="h-32 w-20 flex-none bg-zinc-500 rounded-md"
      >
      </div>
      <div className="flex-3">
        <div>{buildingName}</div>
        <div className="text-sm text-zinc-400">{buildingAddress}</div>
      </div>
      <div className="flex-2 text-center">
        <div className="text-xs text-zinc-500 mb-1">Type</div>
        <div className="">{formatPascalCase(buildingType)}</div>
      </div>
      <div className="flex-2 text-center">
        <div className="text-xs text-zinc-500 mb-1">Status</div>
        <div className="">{formatPascalCase(buildingStatus)}</div>
      </div>
      <div className="flex-1 text-center">
        <div className="text-xs text-zinc-500 mb-1"># Units</div>
        <div className="">{numberOfUnits}</div>
      </div>
      <div className="flex-2 text-center">
        <div className="text-xs text-zinc-500 mb-1">Date Added</div>
        <div className="">{dateAdded}</div>
      </div>
      <div className="relative">
        <button 
          className="flex-none w-8 text-center cursor-pointer" 
          onClick={() => setIsDropdownOpen(!isDropdownOpen)}
        >
          ...
        </button>
        {isDropdownOpen && (
          <ActionsDropdown 
            onMouseLeave={() => setIsDropdownOpen(false)}
            onEditRequest={() => onEditRequest(building)}
            onDelete={() => setIsDeleteModalOpen(true)}
          />
        )}
      </div>
      {isDeleteModalOpen && (
        <ConfirmDeleteModal 
          onConfirm={() => {setIsDeleteModalOpen(false); handleDelete()}} 
          onCancel={() => setIsDeleteModalOpen(false)} 
          entityName={buildingName}
        />
      )}
    </div>
  )
}