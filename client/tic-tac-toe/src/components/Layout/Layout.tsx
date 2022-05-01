import React, {ReactElement} from 'react';
import styles from './Layout.module.scss';
import Header from "./Header";

interface LayoutProps {
    children: ReactElement | ReactElement[]
}

const Layout: React.FC<LayoutProps> = ({children}) => {
    const appName: string = "Tic Tac Toe";

    return (
        <div className={styles.layout}>
            <div className={styles.layout__container}>
                <Header title={appName}/>
                <div className={styles.layout__main}>
                    {children}
                </div>
            </div>
        </div>
    )
}

export default Layout;