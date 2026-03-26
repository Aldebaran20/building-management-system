import type { ReactNode } from 'react'

type ButtonVariant = 'primary' | 'danger' | 'ghost'

export function Button({ children, variant = 'primary', type = 'button', className = '', onClick }: {
  children: ReactNode,
  variant?: ButtonVariant,
  type?: 'button' | 'submit'
  className?: string
  onClick?: () => void
}) {
  const variants: Record<ButtonVariant, string> = {
    primary: 'bg-indigo-600 hover:bg-indigo-500',
    danger: 'bg-red-700 hover:bg-red-600',
    ghost: 'bg-zinc-700 hover:bg-zinc-600'
  }

  return (
    <button
      type={type}
      onClick={onClick}
      className={`cursor-pointer px-4 py-2 rounded-md text-sm font-medium text-white transition-colors duration-150 active:scale-95 ${variants[variant]} ${className}`}
    >
      {children}
    </button>
  )
}