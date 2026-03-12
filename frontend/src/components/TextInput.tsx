export function TextInput({ label, type, name } : {
  label: string,
  type: string
  name: string,
}) {
  return (
    <div>
      <div className="text-sm text-zinc-500 mb-2">{label}</div>
      <input 
        required
        type={type}
        name={name}
        className="p-2 border border-zinc-800 rounded w-full bg-zinc-900"
      />
    </div>
  )
}