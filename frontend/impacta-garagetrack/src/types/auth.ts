export interface LoginRequest {
  email: string
  password: string
}

export interface TokenData {
  accessToken: string
  expiration: string
}

export interface UserData {
  id: number
  name: string
  email: string
  companyId: number
  companyName: string
}

export interface LoginResponseData {
  tokenData: TokenData
  userData: UserData
  companies: unknown
}

export interface AuthState {
  token: string | null
  tokenExpiration: string | null
  user: UserData | null
}
