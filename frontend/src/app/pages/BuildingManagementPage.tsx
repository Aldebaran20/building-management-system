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

  return (
    <div className="bg-zinc-950 text-white">
      <div className="flex items-center justify-between mb-6">
        {!isBuildingFormVisible ? (
          <>
            <div>
              <h1 className="text-2xl font-semibold mb-1">Buildings</h1>
              <p className="text-sm text-zinc-400">Manage your buildings here</p>
            </div>
            <Button onClick={() => setIsBuildingFormVisible(true)}>
              Add Building
            </Button>
          </>
        ) : (
          <>
            <div>
              <h1 className="text-2xl font-semibold mb-1">New Building</h1>
              <p className="text-sm text-zinc-400">Add a new building</p>
            </div>
            <Button variant="danger" onClick={() => setIsBuildingFormVisible(false)}>
              Cancel
            </Button>
          </>
        )}
      </div>

      {isBuildingFormVisible ? (
        <BuildingForm 
          onSuccess={() => { setIsBuildingFormVisible(false); fetchBuildings() }}
        />
      ) : (
        <BuildingList buildings={buildings} onDelete={() => fetchBuildings()} />
      )}
    </div>
  )
}