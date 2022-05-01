import {Reducer, useCallback, useEffect, useMemo, useReducer} from "react";
import {IGame} from "../../models";
import {ClientTag} from "../../constants";
import {GameStatus, Tag} from "../../enums";
import {useHttp} from "../use-http";
import {GameAction, GameActionKind} from "./actions";
import {gameReducer} from "./reducer";

const emptyBoard: (Tag | null)[][] = [[null, null, null], [null, null, null], [null, null, null]];

export type gamePlayerResponse = [
    canPlay: boolean,
    game: IGame,
    tagCell: (row: number, column: number) => void
]


export const useGamePlayer = (gameId: string) : gamePlayerResponse => {
    const apiUrl: string = `${process.env.REACT_APP_API_URL}/api/v1/games/${gameId}`;

    const initialGame = {
        status: GameStatus.Loading,
        id: gameId,
        cells: emptyBoard,
        clientTag: ClientTag
    }

    const [game, dispatchGameAction] = useReducer<Reducer<IGame, GameAction>>(gameReducer, initialGame);

    const [tagIsLoading, tagError, tagResponse, tagCellRequest] = useHttp<IGame>({method: 'POST'})
    const [getIsLoading, getError, gameResponse, getGameRequest] = useHttp<IGame>({method: 'GET', url: apiUrl})

    useEffect(() => {
        getGameRequest();
    }, [gameId])

    useEffect(() => {
        dispatchGameAction({type: GameActionKind.SET, payload: gameResponse});
    }, [gameResponse]);

    useEffect(() => {
        dispatchGameAction({type: GameActionKind.SET, payload: tagResponse});
    }, [tagResponse]);

    useEffect(() => {
        // to simplify single effect
        if(getError)
            alert(getError.message)
        if(tagError)
            alert(tagError.message)
    }, [getError, tagError])


    const canPlay: boolean = useMemo(() =>
        tagIsLoading === false && getIsLoading === false && !!getError && !!tagError,
        [tagIsLoading, getIsLoading, tagError, getError]);

    const tagCell = useCallback((row: number, column: number) => {
        dispatchGameAction({type: GameActionKind.MARK_CELL, payload: {row, column}});

        tagCellRequest({url: `/cells/${row}/${column}`});
    }, [])

    return [
        canPlay,
        game ?? initialGame,
        tagCell,
    ]
}
