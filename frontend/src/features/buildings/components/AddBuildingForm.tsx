import type { SaveBuilding, BuildingType, BuildingStatus } from "@/types"
import { createBuilding } from "../api/create_building"

export function AddBuildingForm({ onSuccess, onCancel }: {
  onSuccess: () => void
  onCancel: () => void
}) {

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    const formData = new FormData(event.currentTarget)

    const building : SaveBuilding = {
      buildingName: formData.get('buildingName') as string,
      buildingAddress: formData.get('buildingAddress') as string,
      numberOfUnits: Number(formData.get('numberOfUnits')),
      buildingType: formData.get('buildingType') as BuildingType,
      buildingStatus: formData.get('buildingStatus') as BuildingStatus,
    } 

    createBuilding(building)
      .then(() => {
        onSuccess()
      })
      .catch((error) => {
        console.error("Error adding building:", error)
      })
  }

  return (
    <form className="mt-20" onSubmit={handleSubmit}>
      <input 
        required
        type="text"
        name="buildingName"
        placeholder="Building Name"
        className="mb-4 p-2 border border-gray-300 rounded w-full"
      />
      <input 
        required
        type="text"
        name="buildingAddress"
        placeholder="Building Address"
        className="mb-4 p-2 border border-gray-300 rounded w-full"
      />
      <input
        required
        type="number"
        name="numberOfUnits"
        placeholder="Number of Units"
        className="mb-4 p-2 border border-gray-300 rounded w-full"
      />
      <input 
        required
        type="text"
        name="buildingType"
        placeholder="Building Type"
        className="mb-4 p-2 border border-gray-300 rounded w-full"
      />
      <input 
        required
        type="text"
        name="buildingStatus"
        placeholder="Building Status"
        className="mb-4 p-2 border border-gray-300 rounded w-full"
      />
      <button
        type="button"
        onClick={onCancel}
        className="mr-4 p-2 bg-gray-300 rounded"
      >
        Cancel
      </button>
      <button
        type="submit"
        className="p-2 bg-blue-500 text-white rounded"
      >
        Submit
      </button>
    </form>
  )
}