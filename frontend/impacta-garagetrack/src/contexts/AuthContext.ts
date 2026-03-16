import { createContext } from 'react'
import type { LoginRequest, UserData } from '../types/auth'

export interface AuthContextData {
  isAuthenticated: boolean
  token: string | null
  user: UserData | null
  login: (payload: LoginRequest) => Promise<void>
  logout: () => void
}

export const AuthContext = createContext<AuthContextData | undefined>(undefined)
