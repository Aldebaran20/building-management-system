import type { Building } from '@/types'
import { formatPascalCase } from '@/utils/format-pascal-case'

export function BuildingCard({
  buildingName,
  buildingAddress,
  numberOfUnits,
  buildingType,
  buildingStatus,
  dateAdded 
}: Building) {

  return (
    <div className="flex border border-zinc-800 rounded-md p-4 h-40 items-center hover:shadow-2xl mb-3 gap-4 bg-zinc-900 hover:bg-zinc-800 transition-colors duration-150">
      <div 
        className="h-32 w-20 flex-none bg-zinc-500 rounded-md"
      >
      </div>
      <div className="flex-3">
        <div>{buildingName}</div>
        <div className="text-sm text-zinc-400">{buildingAddress}</div>
      </div>
      <div className="flex-2 text-center">
        <div className="text-xs text-zinc-500 mb-1">Type</div>
        <div className="">{formatPascalCase(buildingType)}</div>
      </div>
      <div className="flex-2 text-center">
        <div className="text-xs text-zinc-500 mb-1">Status</div>
        <div className="">{formatPascalCase(buildingStatus)}</div>
      </div>
      <div className="flex-1 text-center">
        <div className="text-xs text-zinc-500 mb-1"># Units</div>
        <div className="">{numberOfUnits}</div>
      </div>
      <div className="flex-2 text-center">
        <div className="text-xs text-zinc-500 mb-1">Date Added</div>
        <div className="">{dateAdded}</div>
      </div>
      <button className="flex-none w-8 text-center hover:cursor-pointer">...</button>
    </div>
  )
}