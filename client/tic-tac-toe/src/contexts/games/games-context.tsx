import React, {createContext, ReactElement, Reducer, useCallback, useEffect, useReducer} from "react";
import {IGame} from "../../interfaces";
import {GamesAction, GamesActionKind} from "./action";
import {GameStatus} from "../../enums";
import {contextInitialState, IGamesContextState, IGamesState, reducerInitialState} from "./state";
import {useHttp} from "../../hooks";


interface IGamesContextProps {
    children: ReactElement | ReactElement[]
}

const GamesContext = createContext<IGamesContextState>(contextInitialState);

const gamesReducer = (state: IGamesState, action: GamesAction) => {
    switch (action.type){
        case GamesActionKind.SET:
            return {games: action.payload}
        case GamesActionKind.UPDATE_STATUS:
            return {
                games: state.games.map((game: IGame) =>
                    game.id === action.payload.id ?
                        {...game, status: action.payload.status} : game)
            }
    }
    return {games: []}
}

export const GamesContextProvider: React.FC<IGamesContextProps> = ({children}) => {
    const apiUrl: string = `${process.env.REACT_APP_API_URL}/api/v1/games`;

    const [gamesState, dispatchGamesAction] = useReducer<Reducer<IGamesState, GamesAction>>(gamesReducer, reducerInitialState);
    const [isLoading, error, data, sendRequest] = useHttp<IGame[]>({method: 'GET', url: apiUrl})

    useEffect(() => {
        sendRequest();
    }, []);

    useEffect(() => {
        dispatchGamesAction({type: GamesActionKind.SET, payload: data});
    }, [data])

    const updateGameState = useCallback((id: string, status: GameStatus) =>
            dispatchGamesAction({type: GamesActionKind.UPDATE_STATUS, payload: {id, status}})
        ,  [])

    return (
    <GamesContext.Provider value={{games: gamesState.games ?? [], updateGameState}}>
        {children}
    </GamesContext.Provider>
    );
}

export default GamesContext;