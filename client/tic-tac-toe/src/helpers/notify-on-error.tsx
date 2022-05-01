import {IApiError} from "../models";

export const notifyOnError = (error: IApiError | null): void => {
    console.error(error);
    alert(error?.message || String(error));
}