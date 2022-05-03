import {useCallback, useState} from "react";
import {IApiError, INotification, NotificationType} from "../../models";

export type notificationResponse = [
    notification: INotification | null,
    setNotification: (type: NotificationType) => void,
    setError: (error: IApiError) => void
]

export const useNotification = (): notificationResponse => {
    const [state, setState] = useState(null as INotification | null);

    const setNotification = useCallback((type: NotificationType) => {
        switch (type){
            case NotificationType.GameFailed:
                setState({message: 'Sorry but your game failed. Please open a new game'});
                break;
            case NotificationType.BotWon:
                setState({message:'Oops. You have lost. Next time try harder.'});
                break;
            case NotificationType.ClientWon:
                setState({message:'Congratulations. You won. Keep it up !'});
                break;
            case NotificationType.GameDraw:
                setState({message:'You tied. Next time you will definitely succeed!'});
                break;
        }
    }, []);

    const setError = useCallback((error: IApiError) => {
        console.error(error);
        setState({ message: error?.message || String(error)});
    }, []);

    return [state, setNotification, setError];
}