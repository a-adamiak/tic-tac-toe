import {NavLink} from "react-router-dom";
import {IGame} from "../../../interfaces";
import React, {useContext} from "react";
import styles from './GameList.module.scss';
import GamesContext from "../../../contexts/games/games-context";

const GamesList = () => {

    const gamesState = useContext(GamesContext);

    return (
        <ul className={styles.list}>
            {gamesState.games.map((game: IGame) => (
                <li key={game.id}>
                    <NavLink
                        className={(navData) => (navData.isActive ? `${styles.list__link} ${styles['list__link--active']}` : styles.list__link)}
                        to={`${game.id}`}>
                        {game.id}
                    </NavLink>
                </li>
            ))}
        </ul>
    )
}

export default GamesList;