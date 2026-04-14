import type { Contractor, SaveContractor, ContractorType, ContractorStatus } from '@/types'
import { CONTRACTOR_TYPES, CONTRACTOR_STATUSES } from '@/types'
import { createContractor } from '../api/create_contractor'
import { updateContractor } from '../api/update_contractor'
import { SelectInput } from '@/components/SelectInput'
import { TextInput } from '@/components/TextInput'
import { Button } from '@/components/Button'
import type { FormEvent } from 'react'

export function ContractorForm({ onSuccess, contractor }: {
  onSuccess: () => void
  contractor?: Contractor
}) {
  // Set default values for the form fields based on whether we're editing an existing contractor or creating a new one
  const {
    businessName = '',
    contactName = '',
    contactEmail = '',
    contactPhone = '',
    areaOfOperations = '',
    contractorType = CONTRACTOR_TYPES[0],
    contractorStatus = CONTRACTOR_STATUSES[0],
  } = contractor ?? {}

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    const formData = new FormData(event.currentTarget)

    const savedContractor : SaveContractor = {
      businessName: formData.get('businessName') as string || undefined,
      contactName: formData.get('contactName') as string || undefined,
      contactEmail: formData.get('contactEmail') as string || undefined,
      contactPhone: String(formData.get('contactPhone') ?? ''),
      areaOfOperations: formData.get('areaOfOperations') as string || undefined,
      contractorType: formData.get('contractorType') as ContractorType,
      contractorStatus: formData.get('contractorStatus') as ContractorStatus,
    } 

    if (contractor) {
      updateContractor(contractor.id, savedContractor)
        .then(() => {
          onSuccess()
        })
        .catch((error) => {
          console.error(`Error updating contractor with ID ${contractor.id}:`, error)
        })
    } else {
      createContractor(savedContractor)
        .then(() => {
          onSuccess()
        })
        .catch((error) => {
          console.error(`Error creating contractor:`, error)
        })
    }
  }

  return (
    <form className="flex flex-col gap-8" onSubmit={handleSubmit}>
      <TextInput label="Business Name" type="text" name="businessName" defaultValue={businessName}/>
      <TextInput label="Contact Name" type="text" name="contactName" defaultValue={contactName}/>
      <TextInput label="Contact Email" type="email" name="contactEmail" defaultValue={contactEmail}/>
      <TextInput label="Contact Phone" type="tel" name="contactPhone" required defaultValue={contactPhone}/>
      <TextInput label="Area of Operation" type="text" name="areaOfOperations" defaultValue={areaOfOperations}/>
      <div className="grid grid-cols-3 gap-4">
        <SelectInput label="Contractor Type" name="contractorType" required options={CONTRACTOR_TYPES} defaultValue={contractorType}/>
        <SelectInput label="Contractor Status" name="contractorStatus" required options={CONTRACTOR_STATUSES} defaultValue={contractorStatus}/>
      </div>
      <Button type="submit" className="self-end">{contractor ? "Update Contractor" : "Add Contractor"}</Button>
    </form>
  )
}