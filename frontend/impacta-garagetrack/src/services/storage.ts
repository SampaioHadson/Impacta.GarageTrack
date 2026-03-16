import type { AuthState, UserData } from '../types/auth'

const TOKEN_KEY = 'garage_track_access_token'
const TOKEN_EXPIRATION_KEY = 'garage_track_token_expiration'
const USER_KEY = 'garage_track_user'

function safeParseUserData(value: string | null): UserData | null {
  if (!value) {
    return null
  }

  try {
    return JSON.parse(value) as UserData
  } catch {
    return null
  }
}

export function getAuthStateFromStorage(): AuthState {
  return {
    token: localStorage.getItem(TOKEN_KEY),
    tokenExpiration: localStorage.getItem(TOKEN_EXPIRATION_KEY),
    user: safeParseUserData(localStorage.getItem(USER_KEY)),
  }
}

export function saveAuthStateInStorage(authState: AuthState): void {
  if (authState.token) {
    localStorage.setItem(TOKEN_KEY, authState.token)
  }

  if (authState.tokenExpiration) {
    localStorage.setItem(TOKEN_EXPIRATION_KEY, authState.tokenExpiration)
  }

  if (authState.user) {
    localStorage.setItem(USER_KEY, JSON.stringify(authState.user))
  }
}

export function clearAuthStateFromStorage(): void {
  localStorage.removeItem(TOKEN_KEY)
  localStorage.removeItem(TOKEN_EXPIRATION_KEY)
  localStorage.removeItem(USER_KEY)
}
