import {Route, Routes, Navigate} from "react-router-dom";
import React from "react";
import Home from "../../components/GamesManager/Home";
import styles from './GamesManager.module.scss';
import {GamesContextProvider} from "../../contexts";
import GamesList from "../../containers/GamesList";
import Game from "../../containers/Game";

const GamesManager = () => {
    return (
        <GamesContextProvider>
            <div className={styles.manager}>
                <div className={styles.manager__nav}>
                    <GamesList/>
                </div>
                <Routes>
                    <Route path='/:gameId' element={<Game />} />
                    <Route path='/' element={<Home />} />
                </Routes>
            </div>
        </GamesContextProvider>
    )
}

export default GamesManager;