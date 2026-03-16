import { getAuthStateFromStorage } from './storage'
import type { ApiResponse } from '../types/api'

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? 'https://localhost:65446'

export class HttpError extends Error {
  readonly status: number
  readonly details: unknown

  constructor(message: string, status: number, details?: unknown) {
    super(message)
    this.name = 'HttpError'
    this.status = status
    this.details = details
  }
}

interface RequestOptions extends RequestInit {
  skipAuth?: boolean
}

async function request<T>(path: string, options: RequestOptions = {}): Promise<T> {
  const { token } = getAuthStateFromStorage()

  const headers = new Headers(options.headers)

  if (!headers.has('Content-Type') && options.body) {
    headers.set('Content-Type', 'application/json')
  }

  if (!options.skipAuth && token) {
    headers.set('Authorization', `Bearer ${token}`)
  }

  const response = await fetch(`${API_BASE_URL}${path}`, {
    ...options,
    headers,
  })

  let parsedBody: ApiResponse<T> | null = null

  try {
    parsedBody = (await response.json()) as ApiResponse<T>
  } catch {
    parsedBody = null
  }

  if (!response.ok) {
    const errorMessage = parsedBody?.errorMessage ?? 'Falha na comunicação com a API.'
    throw new HttpError(errorMessage, response.status, parsedBody)
  }

  if (!parsedBody) {
    throw new HttpError('Resposta inválida da API.', response.status)
  }

  if (!parsedBody.success) {
    throw new HttpError(parsedBody.errorMessage ?? 'Operação não concluída.', response.status, parsedBody)
  }

  return parsedBody.data
}

export const httpClient = {
  get: <T>(path: string, options?: RequestOptions) => request<T>(path, { ...options, method: 'GET' }),
  post: <T>(path: string, body?: unknown, options?: RequestOptions) =>
    request<T>(path, {
      ...options,
      method: 'POST',
      body: body ? JSON.stringify(body) : undefined,
    }),
}
