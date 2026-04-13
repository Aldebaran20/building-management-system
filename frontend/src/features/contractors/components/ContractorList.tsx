import type { Contractor } from '@/types'
import { ContractorCard } from './ContractorCard'

export function ContractorList({ contractors, onDelete, onEditRequest }: { 
  contractors: Contractor[] 
  onDelete: () => void
  onEditRequest: (contractor: Contractor) => void
}) {
  return (
    <div className="flex flex-col gap-3">
      {contractors.map((contractor: Contractor) => (
        <ContractorCard key={contractor.id} contractor={contractor} onDelete={onDelete} onEditRequest={onEditRequest} />
      ))}
    </div>
  )
}