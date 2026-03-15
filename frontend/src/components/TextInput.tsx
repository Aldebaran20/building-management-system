export function TextInput({ label, type, name, defaultValue } : {
  label: string,
  type: string
  name: string,
  defaultValue: string | number
}) {
  return (
    <div className="flex flex-col gap-1">
      <label htmlFor={name} className="text-sm text-zinc-500">{label}</label>
      <input 
        id={name}
        required
        type={type}
        name={name}
        defaultValue={defaultValue}
        className="p-2 border border-zinc-800 rounded w-full bg-zinc-900"
      />
    </div>
  )
}