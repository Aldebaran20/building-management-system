import type { SaveBuilding, BuildingType, BuildingStatus } from "@/types"
import { BUILDING_TYPES, BUILDING_STATUSES } from "@/types"
import { createBuilding } from "../api/create_building"
import { SelectInput } from "@/components/SelectInput"
import { TextInput } from "@/components/TextInput"
import { Button } from "@/components/Button"

export function AddBuildingForm({ onSuccess }: {
  onSuccess: () => void
}) {

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    const formData = new FormData(event.currentTarget)

    const building : SaveBuilding = {
      buildingName: formData.get('buildingName') as string ?? '',
      buildingAddress: formData.get('buildingAddress') as string ?? '',
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
    <form className="flex flex-col gap-8 mt-20" onSubmit={handleSubmit}>
      <TextInput label="Building Name" type="text" name="buildingName"/>
      <TextInput label="Building Address"  type="text" name="buildingAddress"/>
      <div className="grid grid-cols-3 gap-4">
        <TextInput label="Number of Units" type="number" name="numberOfUnits"/>
        <SelectInput label="Building Type" name="buildingType" options={BUILDING_TYPES}/>
        <SelectInput  label="Building Status" name="buildingStatus" options={BUILDING_STATUSES}/>
      </div>
      <Button type="submit" className="self-end">Add Building</Button>
    </form>
  )
}