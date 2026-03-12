import type { Building, SaveBuilding } from "@/types"

const API_URL = import.meta.env.VITE_API_URL

export const createBuilding = async (building: SaveBuilding): Promise<Building> => {
    const response = await fetch(`${API_URL}/api/Buildings`, {
        method: 'POST',
        headers: {
            'accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(building),
    })
    return await response.json()}