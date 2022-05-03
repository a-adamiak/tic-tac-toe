import React, { createContext, ReactElement } from 'react'
import {useNotification} from "../../hooks/notifications";
import {IApiError, INotification, NotificationType} from "../../models";

export const contextInitialState = {
  notification: null as INotification | null,
  setNotification: (type: NotificationType) => {},
  setError: (error: IApiError) => {}
}

export type NotificationsContextState = typeof contextInitialState

interface NotificationsContextProps {
  children: ReactElement | ReactElement[]
}
const NotificationsContext = createContext<NotificationsContextState>(contextInitialState)

export const NotificationsContextProvider: React.FC<NotificationsContextProps> = ({
  children,
}) => {
  const [notification, setNotification, setError] = useNotification()

  return (
    <NotificationsContext.Provider
      value={{notification, setNotification, setError}}
    >
      {children}
    </NotificationsContext.Provider>
  )
}

export default NotificationsContext;
