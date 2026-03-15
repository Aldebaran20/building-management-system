import { formatPascalCase } from '@/utils/format-pascal-case'

export function SelectInput({ label, name, options, defaultValue } : {
  label: string,
  name: string,
  options: string[]
  defaultValue: string
}) {
  return (
    <div className="flex flex-col gap-1">
      <label htmlFor={name} className="text-sm text-zinc-500">{label}</label>
      <select 
        id={name}
        required
        name={name}
        defaultValue={defaultValue}
        className="p-2 border border-zinc-800 rounded w-full bg-zinc-900"
      >
        {options.map((option) => (
          <option key={option} value={option}>{formatPascalCase(option)}</option>
        ))}
      </select>
    </div>
  )
}