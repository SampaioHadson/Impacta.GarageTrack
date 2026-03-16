export function formatDateTime(value: string | null): string {
  if (!value) {
    return '-'
  }

  const date = new Date(value)

  if (Number.isNaN(date.getTime())) {
    return value
  }

  return new Intl.DateTimeFormat('pt-BR', {
    dateStyle: 'short',
    timeStyle: 'short',
  }).format(date)
}

export function normalizePlate(input: string): string {
  return input.replace(/[^a-zA-Z0-9]/g, '').toUpperCase()
}
