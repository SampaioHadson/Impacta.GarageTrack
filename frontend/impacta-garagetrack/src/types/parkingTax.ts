export type ParkingTaxType = 'Hour' | 'AfterHour' | 'Daily'

export interface ParkingTax {
  id: number
  type: ParkingTaxType
  minutes: number | null
  value: number
  companyId: number
  createdAt: string
  updatedAt: string
}

export interface ParkingTaxRequest {
  type: ParkingTaxType
  minutes: number | null
  value: number
}

export const PARKING_TAX_TYPE_LABELS: Record<ParkingTaxType, string> = {
  Hour: 'Hora',
  AfterHour: 'Hora Adicional',
  Daily: 'Diária',
}
