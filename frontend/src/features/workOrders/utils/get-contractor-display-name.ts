import type { Contractor, WorkOrderContractor } from "@/types"

export function getContractorDisplayName(contractor: Contractor | WorkOrderContractor): string {
  return contractor.businessName || contractor.contactName || ''
}