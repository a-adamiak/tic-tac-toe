import {useContext, useEffect, useState} from 'react'
import { IApiError } from '../models'
import NotificationsContext from "../contexts/notifications/notifications-context";

export interface IRequestConfig {
  url: string
  method: string
  body?: unknown
}

export function useHttp<T>(): [
  isLoading: boolean,
  error: IApiError | null,
  data: T | null,
  sendRequest: (overwriteConfig: IRequestConfig) => void,
] {
  const genericError = 'UndefinedError'

  const getErrorMessage = (error: unknown): string => {
    if (error instanceof Error) return error.message

    return String(error)
  }

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError | null>(null)
  const [data, setData] = useState<T | null>(null)
  const notificationContext = useContext(NotificationsContext);

  useEffect(() => {
    if(error){
      notificationContext.setError(error);
    }
  }, [error]);

  const sendRequest = async (overwriteConfig: IRequestConfig) => {
    setIsLoading(true)
    setError(null)

    try {
      const response = await fetch(
        overwriteConfig.url,
        {
          method: overwriteConfig.method,
          body: overwriteConfig?.body ? JSON.stringify(overwriteConfig.body) : null,
          headers: new Headers({
            Accept: 'application/json',
            'Content-Type': 'application/json',
          }),
        },
      )

      if (!response.ok) {
        const data = await response.json()
        setError({
          ...data,
          status: response.status,
        })
      } else {
        const data = await response.json()
        setData(data)
      }
    } catch (err: any) {
      setError({
        code: genericError,
        message: getErrorMessage(err),
        status: 500,
      })
    }

    setIsLoading(false)
  }

  return [isLoading, error, data, sendRequest]
}
