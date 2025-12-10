import { useEffect, useState } from 'react'
import type { Building } from '../types'
import { AddBuildingForm } from '../components/AddBuildingForm'
import { BuildingsList } from '../components/BuildingsList'
import './App.css'

export function BuildingManagementPage() {
  const [buildings, setBuildings] = useState<Building[]>([])
  const [isBuildingFormVisible, setIsBuildingFormVisible] = useState(false)

  const fetchBuildings = () => {
    fetch('https://localhost:7090/api/Buildings')
      .then((response) => response.json())
      .then((data) => setBuildings(data))
      .catch((error) => {
        console.error('Error fetching building data:', error)
      })
  }

  useEffect(() => {
    fetchBuildings()
  }, [])

  return (
    <>
      {!isBuildingFormVisible && (
        <button
          className="absolute top-5 right-5"
          onClick={() => setIsBuildingFormVisible(true)}
        >
          Add Building
        </button>
      )}

      {isBuildingFormVisible ? (
        <AddBuildingForm setIsBuildingFormVisible={setIsBuildingFormVisible} fetchBuildings={fetchBuildings} />
      ) : (
        <BuildingsList buildings={buildings} />
      )}
    </>
  )
}