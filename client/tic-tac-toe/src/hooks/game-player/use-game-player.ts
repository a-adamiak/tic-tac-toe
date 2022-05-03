import {Reducer, useCallback, useContext, useEffect, useMemo, useReducer} from 'react'
import {GameStatus, IGame, NotificationType, Tag} from '../../models'
import {useHttp} from '../use-http'
import {GameAction, GameActionKind} from './actions'
import {gameReducer} from './reducer'
import {useNavigate} from 'react-router-dom'
import {ClientTag} from "../../contexts";
import NotificationsContext from "../../contexts/notifications/notifications-context";

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
  const notificationContext = useContext(NotificationsContext);

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
  }, [tagResponse])

  // could be a separate hook
  useEffect(() => {
    switch (tagResponse?.status){
      case GameStatus.BotWon:
        notificationContext.setNotification(NotificationType.BotWon);
        break;
      case GameStatus.Draw:
        notificationContext.setNotification(NotificationType.GameDraw);
        break;
      case GameStatus.Failed:
        notificationContext.setNotification(NotificationType.GameFailed);
        break;
      case GameStatus.ClientWon:
        notificationContext.setNotification(NotificationType.ClientWon);
        break;
    }
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
