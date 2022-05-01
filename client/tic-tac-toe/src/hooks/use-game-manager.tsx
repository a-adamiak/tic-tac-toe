import {Reducer, useCallback, useEffect, useReducer} from "react";
import {IGame} from "../models";
import {useHttp} from "./use-http";
import {ClientTag} from "../constants";
import {GameStatus} from "../enums";

export type IGameMetadata = Pick<IGame, 'id' | 'status'>;

export const reducerInitialState = {
    games: [] as IGameMetadata[]
}
export type GamesState = typeof reducerInitialState;

enum GamesActionKind {
    SET = 'SET',
    UPDATE_STATUS = 'UPDATE_STATUS',
    ADD_GAME = 'ADD_GAME',
    DELETE_GAME = 'DELETE_GAME',
}

interface GamesAction {
    type: GamesActionKind;
    payload: any;
}

const gamesReducer = (state: GamesState, action: GamesAction) => {
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
    }
    return { ...state};
}

export type gameManagerResponse = [
    games: IGameMetadata[],
    updateGameState: (gameId: string, gameState: GameStatus) => void,
    addGame: () => void,
    deleteGame: (gameId: string) => void
]


export const useGameManager = () : gameManagerResponse => {
    const apiUrl: string = `${process.env.REACT_APP_API_URL}/api/v1/games`;

    const [gamesState, dispatchGamesAction] = useReducer<Reducer<GamesState, GamesAction>>(gamesReducer, reducerInitialState);
    const [getIsLoading, getIsError, allGames, getAllRequest] = useHttp<IGame[]>({method: 'GET', url: apiUrl})
    const [postIsLoading, postIsError, createdGameId, createRequest] = useHttp<string>({method: 'POST', url: apiUrl, body: ClientTag})
    const [deleteIsLoading, deleteIsError, deletedGameId, deleteRequest] = useHttp<string>({method: 'DELETE', url: apiUrl})

    useEffect(() => {
        getAllRequest();
    }, []);

    useEffect(() => {
        if(allGames)
            dispatchGamesAction({type: GamesActionKind.SET, payload: allGames});
    }, [allGames])

    useEffect(() => {
        if(createdGameId){
            alert(`Game ${createdGameId} created. Click first game in the list and have fun!`)
            dispatchGamesAction({type: GamesActionKind.ADD_GAME, payload: {gameId: createdGameId}});
        }
        }, [createdGameId])

    useEffect(() => {
        if(deletedGameId){
            alert(`Game ${createdGameId} successfully deleted.`)
            dispatchGamesAction({type: GamesActionKind.DELETE_GAME, payload: {gameId: deletedGameId}});
        }

    }, [deletedGameId])

    useEffect(() => {
        // to simplify single effect
        if(getIsError){
            alert(getIsError.message)
        }
        if(postIsError){
            alert(postIsError)
        }
        if(deleteIsError){
            alert(deleteIsError)
        }
    }, [getIsError, postIsError, deleteIsError])

    const updateGameState = useCallback((gameId: string, status: GameStatus) =>
        dispatchGamesAction({type: GamesActionKind.UPDATE_STATUS, payload: {gameId, status}}),  [])

    const addGame = useCallback(() => createRequest(), [])
    const deleteGame = useCallback((gameId: string) => deleteRequest({url: apiUrl + '/' + gameId}), [])

    return [
        gamesState.games ?? [],
        updateGameState,
        addGame,
        deleteGame
    ]
}
