import { IGameMetadata } from '../../models'

export const reducerInitialState = {
  games: [] as IGameMetadata[],
}
export type GamesState = typeof reducerInitialState
