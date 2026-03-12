type ButtonVariant = 'primary' | 'danger'

export function Button({ children, variant = 'primary', type = 'button', className = '', onClick }: {
  children: React.ReactNode,
  variant?: ButtonVariant,
  type?: 'button' | 'submit'
  className?: string
  onClick?: () => void
}) {
  const variants: Record<ButtonVariant, string> = {
    primary: 'bg-indigo-600 hover:bg-indigo-500',
    danger: 'bg-red-700 hover:bg-red-600',
  }

  return (
    <button
      type={type}
      onClick={onClick}
      className={`px-4 py-2 ${variants[variant]} text-white text-sm font-medium rounded-md transition-colors duration-150 cursor-pointer ${className}`}
    >
      {children}
    </button>
  )
}