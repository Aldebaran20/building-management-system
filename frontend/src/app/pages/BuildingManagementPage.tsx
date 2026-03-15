import { useEffect, useState } from 'react'
import type { Building } from '@/types'
import { getBuildings } from '@/features/buildings/api/get_buildings'
import { BuildingForm } from '@/features/buildings/components/BuildingForm'
import { BuildingList } from '@/features/buildings/components/BuildingList'
import { Button } from '@/components/Button'
import '../App.css'

export function BuildingManagementPage() {
  const [buildings, setBuildings] = useState<Building[]>([])
  const [isBuildingFormVisible, setIsBuildingFormVisible] = useState(false)
  const [editingBuilding, setEditingBuilding] = useState<Building | null>(null)

  const fetchBuildings = () => {
    getBuildings()
      .then((data) => setBuildings(data))
      .catch((error) => {
        console.error('Error fetching building data:', error)
      })
  }

  useEffect(() => {
    fetchBuildings()
  }, [])

  const openAddForm = () => {
    setEditingBuilding(null)
    setIsBuildingFormVisible(true)
  }
  const openEditForm = (building: Building) => {
    setEditingBuilding(building)
    setIsBuildingFormVisible(true)
  }
  const closeForm = () => {
    setIsBuildingFormVisible(false)
    setEditingBuilding(null)
  }

  return (
    <div className="bg-zinc-950 text-white">
      <div className="flex items-center justify-between mb-6">
        {!isBuildingFormVisible ? (
          <>
            <div>
              <h1 className="text-2xl font-semibold mb-1">Buildings</h1>
              <p className="text-sm text-zinc-400">Manage your buildings here</p>
            </div>
            <Button onClick={openAddForm}>
              Add Building
            </Button>
          </>
        ) : (
          <>
            <div>
              <h1 className="text-2xl font-semibold mb-1">
                {editingBuilding ? 'Edit Building' : 'New Building'}
              </h1>
              <p className="text-sm text-zinc-400">
                {editingBuilding ? 'Edit the building details below' : 'Add a new building'}
              </p>
            </div>
            <Button variant="danger" onClick={closeForm}>
              Cancel
            </Button>
          </>
        )}
      </div>

      {isBuildingFormVisible ? (
        <BuildingForm 
          onSuccess={() => { closeForm(); fetchBuildings(); }}
          building={editingBuilding ?? undefined}
        />
      ) : (
        <BuildingList 
          buildings={buildings} 
          onDelete={() => fetchBuildings()} 
          onEditRequest={openEditForm}
        />
      )}
    </div>
  )
}