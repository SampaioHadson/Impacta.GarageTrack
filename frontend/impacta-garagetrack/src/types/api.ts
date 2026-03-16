export interface ApiResponse<T> {
  data: T
  success: boolean
  errorMessage: string | null
  validationErrors: string[] | Record<string, string[]> | null
}
