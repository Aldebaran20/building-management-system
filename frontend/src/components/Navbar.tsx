import { useNavigate } from '@tanstack/react-router'
import { Building, HardHat, ArrowRightFromLine, ArrowLeftFromLine } from 'lucide-react'
import { useState } from 'react'

export function Navbar() {
  const [isExpanded, setIsExpanded] = useState(false)
  const navigate = useNavigate()

  return (
    <nav className={`h-full ${isExpanded ? 'w-36' : 'w-12'} duration-150 py-2 flex flex-col gap-24 bg-zinc-900 text-white`}>
      <div className="flex justify-center">
        <button 
          className={`h-10 w-10 flex items-center justify-center rounded-full cursor-pointer hover:bg-zinc-800`}
          onClick={() => setIsExpanded(prev => !prev)}
        >
          {isExpanded ? <ArrowLeftFromLine size={18} /> : <ArrowRightFromLine size={18} />}
        </button>
      </div>
      <ul className="h-full w-full flex flex-col">
        <li 
          onClick={() => navigate({ to: '/' })} 
          className={`h-12 cursor-pointer flex items-center hover:bg-zinc-800 px-4 gap-2`}
        >
          <Building size={18} className="shrink-0"/>
          <span className={`overflow-hidden duration-150 ${isExpanded ? 'max-w-full' : 'max-w-0'}`}>
            Buildings
          </span>
        </li>
        <li 
          onClick={() => navigate({ to: '/contractors' })} 
          className={`h-12 cursor-pointer flex items-center hover:bg-zinc-800 px-4 gap-2`}
        >
          <HardHat size={18} className="shrink-0"/>
          <span className={`overflow-hidden duration-150 ${isExpanded ? 'max-w-full' : 'max-w-0'}`}>
            Contractors
          </span>
        </li>
      </ul>
    </nav>
  )
}