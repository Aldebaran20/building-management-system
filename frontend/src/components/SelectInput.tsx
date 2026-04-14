import { formatPascalCase } from '@/utils/format-pascal-case'

type SelectOption = string | {
  value: string | number
  label: string
}

export function SelectInput({ label, labelSize = 'sm', name, required=false, options, defaultValue, onChange }: {
  label: string,
  labelSize? : 'xs' | 'sm',
  name: string,
  required?: boolean,
  options: SelectOption[]
  defaultValue: string | number
  onChange?: (event: React.ChangeEvent<HTMLSelectElement>) => void
}) {

  const labelSizeClass = labelSize === 'xs' ? 'text-xs' : 'text-sm'

  return (
    <div className="flex flex-col gap-1">
      <label htmlFor={name} className={`${labelSizeClass} text-zinc-500`}>{label}</label>
      <select 
        id={name}
        required={required}
        name={name}
        defaultValue={defaultValue}
        onChange={onChange}
        className="p-2 border border-zinc-800 rounded w-full bg-zinc-900"
      > 
        {!defaultValue && (
          <option value="">Select an option</option>
        )}
        {options.map((option) => (
          <option key={getOptionValue(option)} value={getOptionValue(option)}>
            {formatPascalCase(getOptionLabel(option))}
          </option>
        ))}
      </select>
    </div>
  )
}

function getOptionValue(option: SelectOption): string {
  return typeof option === 'string' ? option : String(option.value)
}

function getOptionLabel(option: SelectOption): string {
  return typeof option === 'string' ? option : option.label
}