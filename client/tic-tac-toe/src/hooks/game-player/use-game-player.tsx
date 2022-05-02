import { Reducer, useCallback, useEffect, useMemo, useReducer } from 'react'
import { IGame } from '../../models'
import { ClientTag } from '../../constants'
import { GameStatus, Tag } from '../../enums'
import { useHttp } from '../use-http'
import { GameAction, GameActionKind } from './actions'
import { gameReducer } from './reducer'
import { notifyOnStatusChanged } from '../../helpers'
import { useNavigate } from 'react-router-dom'

const emptyBoard: (Tag | null)[][] = [
  [null, null, null],
  [null, null, null],
  [null, null, null],
]

const getInitialState = (gameId: string): IGame => ({
  status: GameStatus.Loading,
  id: gameId,
  cells: emptyBoard,
  clientTag: ClientTag,
});

export type gamePlayerResponse = [
  canPlay: boolean,
  game: IGame,
  tagCell: (row: number, column: number) => void,
]

export const useGamePlayer = (gameId: string): gamePlayerResponse => {
  const apiUrl: string = `${process.env.REACT_APP_API_URL}/api/v1/games/${gameId}`
  const initialGame = getInitialState(gameId);

  const navigate = useNavigate()
  const [game, dispatchGameAction] = useReducer<Reducer<IGame, GameAction>>(
    gameReducer,
    initialGame,
  )

  const [tagIsLoading, tagError, tagResponse, tagCellRequest] = useHttp<IGame>();
  const [getIsLoading, getError, gameResponse, getGameRequest] = useHttp<IGame>();

  useEffect(() => {
    getGameRequest({ method: 'GET', url: apiUrl })
  }, [gameId])

  useEffect(() => {
    dispatchGameAction({ type: GameActionKind.SET, payload: gameResponse })
  }, [gameResponse])

  useEffect(() => {
    dispatchGameAction({ type: GameActionKind.SET, payload: tagResponse })

    notifyOnStatusChanged(tagResponse?.status!)
  }, [tagResponse])

  useEffect(() => {
    if (tagError) {
      getGameRequest({
        method: 'POST',
        url: apiUrl
      })
    }
  }, [tagError, gameId])

  useEffect(() => {
    if (getError?.status === 404)
      navigate('../')
  }, [getError])

  const canPlay: boolean = useMemo(
    () =>
      tagIsLoading === false &&
      getIsLoading === false &&
      !getError &&
      game?.status === GameStatus.InProgress,
    [tagIsLoading, getIsLoading, getError, game],
  )

  const tagCell = useCallback(
    (row: number, column: number) => {
      dispatchGameAction({
        type: GameActionKind.TAG_CELL,
        payload: { row, column },
      })
      tagCellRequest({ url: apiUrl + `/cells/${row}/${column}`, method: 'POST' })
    },
    [apiUrl],
  )

  return [canPlay, game ?? initialGame, tagCell]
}
