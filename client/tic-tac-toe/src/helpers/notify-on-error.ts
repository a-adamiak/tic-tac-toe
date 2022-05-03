import { IApiError } from '../models'

// it could be a hook in complex scenario
export const notifyOnError = (error: IApiError | null): void => {
  console.error(error)
  alert(error?.message || String(error))
}
