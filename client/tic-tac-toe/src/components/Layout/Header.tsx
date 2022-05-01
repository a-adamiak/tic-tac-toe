import React from 'react';
import styles from './Header.module.scss';

interface HeaderProps {
    title: string;
}

const Header: React.FC<HeaderProps> = ({title}) => {
    return (
        <div className={styles.header}>
            <h2 >{title}</h2>
        </div>
    )
}

export default Header;