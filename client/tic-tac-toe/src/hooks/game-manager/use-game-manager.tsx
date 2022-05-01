import {Reducer, useCallback, useEffect, useReducer} from "react";
import {IGame, IGameMetadata} from "../../models";
import {useHttp} from "../use-http";
import {ClientTag} from "../../constants";
import {GameStatus} from "../../enums";
import {GamesAction, GamesActionKind} from "./actions";
import {GamesState, reducerInitialState} from "./state";
import {gamesReducer} from "./reducer";
import {notifyOnError} from "../../helpers";
import {useLocation, useNavigate} from "react-router-dom";

export type gameManagerResponse = [
    games: IGameMetadata[],
    updateGameState: (gameId: string, gameState: GameStatus) => void,
    addGame: () => void,
    deleteGame: (gameId: string) => void
]


export const useGameManager = () : gameManagerResponse => {
    const apiUrl: string = `${process.env.REACT_APP_API_URL}/api/v1/games`;

    const navigate = useNavigate();
    const location = useLocation();
    const [gamesState, dispatchGamesAction] = useReducer<Reducer<GamesState, GamesAction>>(gamesReducer, reducerInitialState);
    // to simplify I don't use the loading state
    const [getIsLoading, getError, allGames, getAllRequest] = useHttp<IGame[]>({method: 'GET', url: apiUrl})
    const [postIsLoading, postError, createdGameId, createRequest] = useHttp<string>({method: 'POST', url: apiUrl, body: ClientTag})
    const [deleteIsLoading, deleteError, deletedGameId, deleteRequest] = useHttp<string>({method: 'DELETE', url: apiUrl})

    useEffect(() => {
        getAllRequest();
    }, []);

    useEffect(() => {
        if(allGames)
            dispatchGamesAction({type: GamesActionKind.SET, payload: allGames});
    }, [allGames])

    useEffect(() => {
        if(createdGameId){
            dispatchGamesAction({type: GamesActionKind.ADD_GAME, payload: {gameId: createdGameId}});
            navigate(createdGameId);
        }
        }, [createdGameId])

    useEffect(() => {
        if(deletedGameId){
            dispatchGamesAction({type: GamesActionKind.DELETE_GAME, payload: {gameId: deletedGameId}});
            if(location.pathname.includes(deletedGameId)){
                navigate('/games');
            }
        }

    }, [deletedGameId])

    useEffect(() => {
        if(getError)
            notifyOnError(getError);
    }, [getError])
    useEffect(() => {
        if(postError)
            notifyOnError(postError);
    }, [postError])
    useEffect(() => {
        if(deleteError)
            notifyOnError(deleteError);
    }, [deleteError])

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
