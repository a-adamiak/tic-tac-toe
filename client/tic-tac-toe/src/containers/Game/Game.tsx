import Board from "../../components/Board";
import {useGamePlayer} from "../../hooks";
import {useParams} from "react-router-dom";
import styles from "./Game.module.scss";

const Game = () => {
    const params = useParams();
    const { gameId } = params;

    const [canPlay, game, tagCell] = useGamePlayer(gameId as string);

    return (
        <div className={styles.game}>
           <Board
               id={game.id}
               cells={game.cells}
               clientTag={game.clientTag}
               status={game.status}
               canPlay={canPlay}
               tagCell={tagCell}
           />
        </div>
    )
}

export default Game;