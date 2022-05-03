import React, {FC, ReactElement, useContext, useEffect} from "react";
import NotificationsContext from "../../contexts/notifications/notifications-context";

interface NotificationProps {
    children: ReactElement | ReactElement[]
}

const Notifications: FC<NotificationProps> = ({ children }) => {
    const context = useContext(NotificationsContext);

    useEffect(() => {
        if(context.notification?.message){
            alert(context.notification.message)
        }
    }, [context.notification]);

    return (
        <>
            {children}
       </>
    )
}

export default Notifications;
