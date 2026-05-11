export type ParkingTaxType = 'Hour' | 'AfterHour' | 'Daily'

export interface ParkingRecord {
  id: number
  plate: string
  color: string
  entryTime: string
  exitTime: string | null
  totalValue: number | null
  createdByUserId: number
  createdByUserName: string
  finishedByUserId: number | null
  finishedByUserName: string | null
}

export interface CreateParkingRequest {
  plate: string
  color: string
}

export interface AppliedTaxDetail {
  type: ParkingTaxType
  description: string
  value: number
}

export interface CloseParkingResult {
  id: number
  plate: string
  color: string
  entryTime: string
  exitTime: string
  totalMinutes: number
  appliedTaxes: AppliedTaxDetail[]
  totalValue: number
}
