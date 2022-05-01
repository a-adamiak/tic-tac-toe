import {Route, Routes} from "react-router-dom";
import React from "react";
import Home from "../../components/GamesManager/Home";
import GamesList from "../../components/GamesManager/GamesList";
import Game from '../Game/Game';
import styles from './GamesManager.module.scss';
import {GamesContextProvider} from "../../contexts";

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