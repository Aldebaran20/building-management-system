import type { Building } from "@/types"

const API_URL = import.meta.env.VITE_API_URL

export const getBuildings = async (): Promise<Building[]> => {
    const response = await fetch(`${API_URL}/api/Buildings`)
    return await response.json()}