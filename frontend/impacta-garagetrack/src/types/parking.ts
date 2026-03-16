export interface ParkingRecord {
  id: number
  plate: string
  color: string
  entryTime: string
  exitTime: string | null
  createdByUserId: number
  createdByUserName: string
  finishedByUserId: number | null
  finishedByUserName: string | null
}

export interface CreateParkingRequest {
  plate: string
  color: string
}
