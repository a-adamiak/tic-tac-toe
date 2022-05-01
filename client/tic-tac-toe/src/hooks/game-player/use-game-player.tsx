import { Reducer, useCallback, useEffect, useMemo, useReducer } from 'react'
import { IGame } from '../../models'
import { ClientTag } from '../../constants'
import { GameStatus, Tag } from '../../enums'
import { useHttp } from '../use-http'
import { GameAction, GameActionKind } from './actions'
import { gameReducer } from './reducer'
import { notifyOnError, notifyOnStatusChanged } from '../../helpers'
import { useNavigate } from 'react-router-dom'

const emptyBoard: (Tag | null)[][] = [
  [null, null, null],
  [null, null, null],
  [null, null, null],
]

export type gamePlayerResponse = [
  canPlay: boolean,
  game: IGame,
  tagCell: (row: number, column: number) => void,
]

export const useGamePlayer = (gameId: string): gamePlayerResponse => {
  const apiUrl: string = `${process.env.REACT_APP_API_URL}/api/v1/games/${gameId}`

  const navigate = useNavigate()

  const initialGame = {
    status: GameStatus.Loading,
    id: gameId,
    cells: emptyBoard,
    clientTag: ClientTag,
  }

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
    if (getError)
      getError.status === 404 ? navigate('../') : notifyOnError(getError)
  }, [getError])

  useEffect(() => {
    dispatchGameAction({ type: GameActionKind.SET, payload: tagResponse })

    notifyOnStatusChanged(tagResponse?.status!)
  }, [tagResponse])

  useEffect(() => {
    if (tagError) {
      notifyOnError(tagError)
      getGameRequest({
        method: 'POST',
        url: apiUrl
      })
    }
  }, [tagError, gameId])

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
        type: GameActionKind.MARK_CELL,
        payload: { row, column },
      })
      tagCellRequest({ url: apiUrl + `/cells/${row}/${column}`, method: 'POST' })
    },
    [apiUrl],
  )

  return [canPlay, game ?? initialGame, tagCell]
}
