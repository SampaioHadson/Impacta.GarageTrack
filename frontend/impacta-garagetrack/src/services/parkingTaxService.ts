import { httpClient } from './httpClient'
import type { ParkingTax, ParkingTaxRequest } from '../types/parkingTax'

export const parkingTaxService = {
  list: () => httpClient.get<ParkingTax[]>('/parkingtax'),
  create: (payload: ParkingTaxRequest) =>
    httpClient.post<ParkingTax>('/parkingtax', payload),
  update: (id: number, payload: ParkingTaxRequest) =>
    httpClient.put<ParkingTax>(`/parkingtax/${id}`, payload),
  remove: (id: number) => httpClient.delete<null>(`/parkingtax/${id}`),
}
