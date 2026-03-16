import type { Building, SaveBuilding, BuildingType, BuildingStatus } from "@/types"
import { BUILDING_TYPES, BUILDING_STATUSES } from "@/types"
import { createBuilding } from "../api/create_building"
import { updateBuilding } from "../api/update_building"
import { SelectInput } from "@/components/SelectInput"
import { TextInput } from "@/components/TextInput"
import { Button } from "@/components/Button"
import type { FormEvent } from "react"

export function BuildingForm({ onSuccess, building }: {
  onSuccess: () => void
  building?: Building
}) {
  // Set default values for the form fields based on whether we're editing an existing building or creating a new one
  const {
    buildingName = '',
    buildingAddress = '',
    numberOfUnits = 0,
    buildingType = BUILDING_TYPES[0],
    buildingStatus = BUILDING_STATUSES[0],
  } = building ?? {}

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    const formData = new FormData(event.currentTarget)

    const savedBuilding : SaveBuilding = {
      buildingName: String(formData.get('buildingName') ?? ''),
      buildingAddress: String(formData.get('buildingAddress') ?? ''),
      numberOfUnits: Number(formData.get('numberOfUnits')),
      buildingType: formData.get('buildingType') as BuildingType,
      buildingStatus: formData.get('buildingStatus') as BuildingStatus,
    } 

    if (building) {
      updateBuilding(building.id, savedBuilding)
        .then(() => {
          onSuccess()
        })
        .catch((error) => {
          console.error("Error updating building:", error)
        })
    } else {
      createBuilding(savedBuilding)
        .then(() => {
          onSuccess()
        })
        .catch((error) => {
          console.error("Error adding building:", error)
        })
    }
  }

  return (
    <form className="flex flex-col gap-8 mt-20" onSubmit={handleSubmit}>
      <TextInput label="Building Name" type="text" name="buildingName" defaultValue={buildingName}/>
      <TextInput label="Building Address"  type="text" name="buildingAddress" defaultValue={buildingAddress}/>
      <div className="grid grid-cols-3 gap-4">
        <TextInput label="Number of Units" type="number" name="numberOfUnits" defaultValue={numberOfUnits}/>
        <SelectInput label="Building Type" name="buildingType" options={BUILDING_TYPES} defaultValue={buildingType}/>
        <SelectInput  label="Building Status" name="buildingStatus" options={BUILDING_STATUSES} defaultValue={buildingStatus}/>
      </div>
      <Button type="submit" className="self-end">{building ? "Update Building" : "Add Building"}</Button>
    </form>
  )
}