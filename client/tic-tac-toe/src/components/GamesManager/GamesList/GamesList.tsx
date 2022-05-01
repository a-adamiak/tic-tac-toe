import {NavLink} from "react-router-dom";
import React, {useCallback, useContext} from "react";
import styles from './GameList.module.scss';
import GamesContext from "../../../contexts/games/games-context";
import AddButton from "../AddButton";
import {IGameMetadata} from "../../../hooks";
import DeleteButton from "../DeleteButton";
import GamesListItem from "../GamesListItem";

const GamesList = () => {
    const gamesState = useContext(GamesContext);

    const createGame = useCallback(() => {
        gamesState.addGame();
    }, []);

    const deleteGame = useCallback((gameId: string) => {
        gamesState.deleteGame(gameId);
    }, []);

    return (
        <>
            <div className={styles.header}>
                <span>List of games</span>
                <AddButton onClick={createGame}></AddButton>
            </div>

                <ul className={styles.list}>
                    {[...gamesState.games].reverse().map((game: IGameMetadata) => (
                        <GamesListItem
                            key={game.id}
                            deleteGame={deleteGame}
                            id={game.id}
                            status={game.status}
                        />
                    ))}
                </ul>
        </>


    )
}

export default GamesList;