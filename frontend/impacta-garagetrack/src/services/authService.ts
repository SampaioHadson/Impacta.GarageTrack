import { httpClient } from './httpClient'
import type { LoginRequest, LoginResponseData } from '../types/auth'

export const authService = {
  login: (payload: LoginRequest) =>
    httpClient.post<LoginResponseData>('/auth/login', payload, { skipAuth: true }),
}
