import { useEffect, useState } from 'react'
import type { Building } from '@/types'
import { getBuildings } from '@/features/buildings/api/get_buildings'
import { BuildingForm } from '@/features/buildings/components/BuildingForm'
import { BuildingList } from '@/features/buildings/components/BuildingList'
import { Button } from '@/components/Button'
import { createRoute, redirect } from '@tanstack/react-router'
import { authenticatedRoute } from '@/app/root-route'

export const buildingsRoute = createRoute({
  getParentRoute: () => authenticatedRoute,
  path: '/',
  beforeLoad: () => {
    if (!sessionStorage.getItem('token')) {
      throw redirect({ to: '/login' })
    }
  },
  component: BuildingsPage,
})

function BuildingsPage() {
  const [buildings, setBuildings] = useState<Building[]>([])
  const [isBuildingFormVisible, setIsBuildingFormVisible] = useState(false)
  const [editingBuilding, setEditingBuilding] = useState<Building | null>(null)

  const fetchBuildings = () => {
    getBuildings()
      .then((data) => setBuildings(data))
      .catch((error) => {
        console.error(`Error fetching buildings: ${error}`)
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

  const renderHeader = () => {
    if (!isBuildingFormVisible) {
      return (
        <>
          <div>
            <h1 className="text-2xl font-semibold mb-1">Buildings</h1>
            <p className="text-sm text-zinc-400">Manage your buildings here</p>
          </div>
          <Button onClick={openAddForm}>Add Building</Button>
        </>
      );
    }

    return (
      <>
        <div>
          <h1 className="text-2xl font-semibold mb-1">
            {editingBuilding ? 'Edit Building' : 'New Building'}
          </h1>
          <p className="text-sm text-zinc-400">
            {editingBuilding ? 'Edit the building details below' : 'Add a new building'}
          </p>
        </div>
        <Button variant="danger" onClick={closeForm}>Cancel</Button>
      </>
    );
  }

  const renderContent = () => {
    if (isBuildingFormVisible) {
      return (
        <BuildingForm 
          onSuccess={() => { closeForm(); fetchBuildings(); }}
          building={editingBuilding ?? undefined}
        />
      );
    }

    if (buildings.length === 0) {
      return (
        <div className="text-center flex flex-col gap-2 mt-52">
          <h1 className="text-4xl font-semibold">No buildings yet</h1>
          <p className="text-md text-zinc-400">Add your first building to get started</p>
        </div>
      );
    }

    return (
      <BuildingList 
        buildings={buildings} 
        onDelete={() => fetchBuildings()} 
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