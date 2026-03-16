import { useCallback, useMemo, useState, type ReactNode } from 'react'
import { AuthContext, type AuthContextData } from '../contexts/AuthContext'
import { authService } from '../services/authService'
import {
  clearAuthStateFromStorage,
  getAuthStateFromStorage,
  saveAuthStateInStorage,
} from '../services/storage'
import type { LoginRequest } from '../types/auth'

export function AuthProvider({ children }: { children: ReactNode }) {
  const [authState, setAuthState] = useState(() => getAuthStateFromStorage())

  const login = useCallback(async (payload: LoginRequest) => {
    const response = await authService.login(payload)

    const nextState = {
      token: response.tokenData.accessToken,
      tokenExpiration: response.tokenData.expiration,
      user: response.userData,
    }

    saveAuthStateInStorage(nextState)
    setAuthState(nextState)
  }, [])

  const logout = useCallback(() => {
    clearAuthStateFromStorage()
    setAuthState({ token: null, tokenExpiration: null, user: null })
  }, [])

  const value = useMemo<AuthContextData>(
    () => ({
      isAuthenticated: Boolean(authState.token),
      token: authState.token,
      user: authState.user,
      login,
      logout,
    }),
    [authState.token, authState.user, login, logout],
  )

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
}
