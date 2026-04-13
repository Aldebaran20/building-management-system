export function TextInput({ label, type, name, required=false, placeholder, defaultValue } : {
  label: string,
  type: string
  name: string,
  required?: boolean,
  placeholder?: string,
  defaultValue?: string | number
}) {
  return (
    <div className="flex flex-col gap-1">
      <label htmlFor={name} className="text-sm text-zinc-500">{label}</label>
      <input 
        id={name}
        required={required}
        type={type}
        name={name}
        placeholder={placeholder}
        defaultValue={defaultValue}
        className="p-2 border border-zinc-600 rounded-lg w-full bg-zinc-800"
      />
    </div>
  )
}