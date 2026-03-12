export function formatPascalCase(option: string): string {
  return option.replace(/([A-Z])/g, ' $1').trim()
}