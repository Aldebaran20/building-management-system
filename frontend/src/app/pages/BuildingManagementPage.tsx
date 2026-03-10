import { useEffect, useState } from 'react'
import type { Building } from '../../types'
import { getBuildings } from '../../features/buildings/api/get_buildings'
import { AddBuildingForm } from '../../features/buildings/components/AddBuildingForm'
import { BuildingList } from '../../features/buildings/components/BuildingList'
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
        <div>
          <h1 className="text-2xl font-semibold mb-1">Buildings</h1>
          <p className="text-sm text-zinc-400">Manage your buildings here</p>
        </div>
        {!isBuildingFormVisible && (
          <button
            className="px-4 py-2 bg-indigo-600 hover:bg-indigo-500 text-white text-sm font-medium rounded-md transition-colors duration-150 cursor-pointer"
            onClick={() => setIsBuildingFormVisible(true)}
          >
            Add Building
          </button>
        )}
      </div>

      {isBuildingFormVisible ? (
        <AddBuildingForm setIsBuildingFormVisible={setIsBuildingFormVisible} fetchBuildings={fetchBuildings} />
      ) : (
        <BuildingList buildings={buildings} />
      )}
    </div>
  )
}