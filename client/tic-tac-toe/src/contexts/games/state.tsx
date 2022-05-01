import {IGame} from "../../interfaces";
import {GameStatus} from "../../enums";

export const contextInitialState = {
    games: [] as IGame[],
    updateGameState: (gameId: string, gameState: GameStatus) => {}
};
export const reducerInitialState = {
    games: [] as IGame[]
}

export type IGamesContextState = typeof contextInitialState;
export type IGamesState = typeof reducerInitialState;