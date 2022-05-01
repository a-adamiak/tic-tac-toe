import {useState} from "react";
import {IApiError} from "../models";

export interface IRequestConfig {
    url?: string;
    method?: string;
    body?: unknown;
}

export function useHttp<T> (requestConfig: IRequestConfig): [isLoading: boolean, error: IApiError | null, data: T | null, sendRequest: (overwriteConfig?: IRequestConfig) => void] {
    const genericError = 'UndefinedError';

    const getErrorMessage = (error: unknown): string => {
        if (error instanceof Error)
            return error.message

        return String(error);
    }

    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<IApiError | null>(null);
    const [data, setData] = useState<T | null>(null);

    const sendRequest = async (overwriteConfig?: IRequestConfig) => {
        setIsLoading(true);
        setError(null);

        try {
            const response = await fetch(
                overwriteConfig?.url ?? requestConfig.url ?? '',
                {
                    method: requestConfig.method,
                    body: (overwriteConfig?.body ?? requestConfig.body) ? JSON.stringify(requestConfig.body) : null,
                    headers: new Headers({
                        "Accept": "application/json",
                        "Content-Type": "application/json",
                    })
                }
            );

            if(!response.ok) {
                const data = await response.json();
                setError(data)
            }
            else {
                const data = await response.json();
                setData(data);
            }
        }
        catch (err: any) {
            setError({code: genericError, message: getErrorMessage(err)})
        }

        setIsLoading(false);
    }

    return [
        isLoading,
        error,
        data,
        sendRequest
    ]
}
