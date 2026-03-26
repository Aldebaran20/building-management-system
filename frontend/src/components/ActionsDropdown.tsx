import { Pencil, Trash2 } from 'lucide-react'

export function ActionsDropdown({ onMouseLeave, onEditRequest, onDelete }: {
  onMouseLeave: () => void
  onEditRequest: () => void
  onDelete: () => void
}) {
  return (
    <div 
      className="flex flex-col absolute top-8 right-0 border border-zinc-500 bg-zinc-800 rounded-md"
      onMouseLeave={onMouseLeave}
    >
      <button 
        className="p-2 cursor-pointer hover:bg-zinc-700 rounded-t-md flex items-center gap-2"
        onClick={onEditRequest}
      >
        <Pencil size={14} /> Edit
      </button>
      <button 
        className="p-2 cursor-pointer hover:bg-zinc-700 rounded-b-md flex items-center gap-2 text-red-400"
        onClick={onDelete}
      >
        <Trash2 size={14} /> Delete
      </button>
    </div>
  )
}