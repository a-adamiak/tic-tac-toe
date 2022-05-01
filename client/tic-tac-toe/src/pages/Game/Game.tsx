import {IGame} from "../../models";
import {GameStatus, Tag} from "../../enums";
import Board from "../../components/Board";

const Game = () => {
    const game: IGame = {
        status: GameStatus.InProgress,
        id: 'test',
        clientTag: Tag.X,
        cells: [[null, null, null], [null, null, null], [null, null, null]]
    }

    return (
        <div>
           <Board id={game.id} cells={game.cells} clientTag={game.clientTag} status={game.status}></Board>
        </div>
    )
}

export default Game;