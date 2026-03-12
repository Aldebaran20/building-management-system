import { formatPascalCase } from '@/utils/format-pascal-case'

export function SelectInput({ label, name, options } : {
  label: string,
  name: string,
  options: string[]
}) {
  return (
    <div>
      <label htmlFor={name} className="text-sm text-zinc-500 mb-2">{label}</label>
      <select 
        id={name}
        required
        name={name}
        className="p-2 border border-zinc-800 rounded w-full bg-zinc-900"
      >
        {options.map((option) => (
          <option key={option} value={option}>{formatPascalCase(option)}</option>
        ))}
      </select>
    </div>
  )
}