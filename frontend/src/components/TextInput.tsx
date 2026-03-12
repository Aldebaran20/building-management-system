export function TextInput({ label, type, name } : {
  label: string,
  type: string
  name: string,
}) {
  return (
    <div>
      <label htmlFor={name} className="text-sm text-zinc-500 mb-2">{label}</label>
      <input 
        id={name}
        required
        type={type}
        name={name}
        className="p-2 border border-zinc-800 rounded w-full bg-zinc-900"
      />
    </div>
  )
}