import { Button } from "./Button"

export function ConfirmDeleteModal({ onConfirm, onCancel, entityName }: {
  onConfirm: () => void
  onCancel: () => void
  entityName: string
}) {
  return (
    <div className="fixed inset-0 bg-black/50 flex items-center justify-center">
      <div className="flex flex-col gap-4 bg-zinc-800 p-6 rounded-md">
        <h2 className="text-lg">Confirm Deletion</h2>
        <p>Are you sure you want to delete {entityName}?</p>
        <div className="flex justify-end gap-2">
          <Button
            variant="ghost"
            onClick={onCancel}
          >
            Cancel
          </Button>
          <Button
            variant="danger"
            onClick={onConfirm}
          >
            Delete
          </Button>
        </div>
      </div>
    </div>
  )
}