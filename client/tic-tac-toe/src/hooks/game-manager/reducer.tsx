import {GamesState} from "./state";
import {GamesAction, GamesActionKind} from "./actions";
import {GameStatus} from "../../enums";
import {IGameMetadata} from "../../models";

export const gamesReducer = (state: GamesState, action: GamesAction) => {
    switch (action.type){
        case GamesActionKind.SET:
            return {games: action.payload}
        case GamesActionKind.UPDATE_STATUS:
            return {
                games: state.games.map((game: Partial<IGameMetadata>) =>
                    game.id === action.payload.id ?
                        {...game, status: action.payload.status} : game)
            }
        case GamesActionKind.ADD_GAME:
            return {
                games: [...state.games, {id: action.payload.gameId, status: GameStatus.InProgress}]
            }
        case GamesActionKind.DELETE_GAME:
            return {
                games: state.games.filter((e: Partial<IGameMetadata>) => e.id != action.payload.gameId)
            }
        default:
            return { ...state};
    }

}