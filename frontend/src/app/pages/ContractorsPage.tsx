import { useEffect, useState } from 'react'
import type { Contractor } from '@/types'
import { getContractors } from '@/features/contractors/api/get_contractors'
import { ContractorForm } from '@/features/contractors/components/ContractorForm'
import { ContractorList } from '@/features/contractors/components/ContractorList'
import { Button } from '@/components/Button'
import { createRoute, redirect } from '@tanstack/react-router'
import { authenticatedRoute } from '@/app/root-route'

export const contractorsRoute = createRoute({
  getParentRoute: () => authenticatedRoute,
  path: '/contractors',
  beforeLoad: () => {
    if (!sessionStorage.getItem('token')) {
      throw redirect({ to: '/login' })
    }
  },
  component: ContractorsPage,
})

function ContractorsPage() {
  const [contractors, setContractors] = useState<Contractor[]>([])
  const [isContractorFormVisible, setIsContractorFormVisible] = useState(false)
  const [editingContractor, setEditingContractor] = useState<Contractor | null>(null)

  const fetchContractors = () => {
    getContractors()
      .then((data) => setContractors(data))
      .catch((error) => {
        console.error(`Error fetching contractors: ${error}`)
      })
  }

  useEffect(() => {
    fetchContractors()
  }, [])

  const openAddForm = () => {
    setEditingContractor(null)
    setIsContractorFormVisible(true)
  }
  const openEditForm = (contractor: Contractor) => {
    setEditingContractor(contractor)
    setIsContractorFormVisible(true)
  }
  const closeForm = () => {
    setIsContractorFormVisible(false)
    setEditingContractor(null)
  }

  const renderHeader = () => {
    if (!isContractorFormVisible) {
      return (
        <>
          <div>
            <h1 className="text-2xl font-semibold mb-1">Contractors</h1>
            <p className="text-sm text-zinc-400">Manage your contractors here</p>
          </div>
          <Button onClick={openAddForm}>Add Contractor</Button>
        </>
      );
    }

    return (
      <>
        <div>
          <h1 className="text-2xl font-semibold mb-1">
            {editingContractor ? 'Edit Contractor' : 'New Contractor'}
          </h1>
          <p className="text-sm text-zinc-400">
            {editingContractor ? 'Edit the contractor details below' : 'Add a new contractor'}
          </p>
        </div>
        <Button variant="danger" onClick={closeForm}>Cancel</Button>
      </>
    );
  }

  const renderContent = () => {
    if (isContractorFormVisible) {
      return (
        <ContractorForm 
          onSuccess={() => { closeForm(); fetchContractors(); }}
          contractor={editingContractor ?? undefined}
        />
      );
    }

    if (contractors.length === 0) {
      return (
        <div className="text-center flex flex-col gap-2 mt-52">
          <h1 className="text-4xl font-semibold">No contractors yet</h1>
          <p className="text-md text-zinc-400">Add your first contractor to get started</p>
        </div>
      );
    }

    return (
      <ContractorList 
        contractors={contractors} 
        onDelete={() => fetchContractors()} 
        onEditRequest={openEditForm}
      />
    );
  }

  return (
    <div className="py-8 px-10 bg-zinc-950 text-white flex flex-col gap-14">
      <div className="flex items-center justify-between">
        {renderHeader()}
      </div>
      {renderContent()}
    </div>
  )
}