import { httpClient } from './httpClient'
import type { CreateParkingRequest, ParkingRecord } from '../types/parking'

export const parkingService = {
  list: () => httpClient.get<ParkingRecord[]>('/parking'),
  getById: (id: number) => httpClient.get<ParkingRecord>(`/parking/${id}`),
  create: (payload: CreateParkingRequest) =>
    httpClient.post<ParkingRecord>('/parking', payload),
}
