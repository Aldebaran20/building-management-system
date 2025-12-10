export function AddBuildingForm({ setIsBuildingFormVisible, fetchBuildings }: {
  setIsBuildingFormVisible: React.Dispatch<React.SetStateAction<boolean>>, 
  fetchBuildings: () => void 
}) {

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    const formData = new FormData(event.currentTarget)

    const building = {
      buildingName: formData.get('buildingName'),
      buildingAddress: formData.get('buildingAddress'),
      numberOfUnits: formData.get('numberOfUnits'),
      buildingType: formData.get('buildingType'),
      buildingStatus: formData.get('buildingStatus'),
    }

    fetch('https://localhost:7090/api/Buildings', {
      method: 'POST',
      headers: {
        'accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(building),
    })
      .then((response) => response.json())
      .then((data) => {
        console.log('Success:', data)
        setIsBuildingFormVisible(false)
        fetchBuildings()
      })
      .catch((error) => {
        console.error("Error adding building:", error)
      })
  }

  return (
    <form className="mt-20" onSubmit={(e: React.FormEvent<HTMLFormElement>) => handleSubmit(e)}>
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
        onClick={() => setIsBuildingFormVisible(false)}
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