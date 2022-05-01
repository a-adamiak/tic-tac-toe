import React, {useState} from "react";
import {IError} from "../interfaces";

export interface IRequestConfig {
    url: string;
    method: string;
    body?: unknown;
}

export function useHttp<T> (requestConfig: IRequestConfig): [isLoading: boolean, error: IError | null, data: T | null, sendRequest: () => void] {
    const genericError = 'UndefinedError';

    const getErrorMessage = (error: unknown): string => {
        if (error instanceof Error)
            return error.message

        return String(error);
    }

    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<IError | null>(null);
    const [data, setData] = useState<T | null>(null);

    const sendRequest = async () => {
        setIsLoading(true);
        setError(null);

        try {
            const response = await fetch(
                requestConfig.url,
                {
                    method: requestConfig.method,
                    body: requestConfig.body ? JSON.stringify(requestConfig.body) : null
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
