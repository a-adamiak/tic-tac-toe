import React, { createContext, ReactElement } from 'react'
import { useGameManager } from '../../hooks'
import {GameStatus, IGameMetadata} from '../../models'

export const contextInitialState = {
  games: [] as IGameMetadata[],
  updateGameState: (gameId: string, gameState: GameStatus) => {},
  addGame: () => {},
  deleteGame: (gameId: string) => {},
}
export type GamesContextState = typeof contextInitialState

interface GamesContextProps {
  children: ReactElement | ReactElement[]
}
const GamesContext = createContext<GamesContextState>(contextInitialState)

export const GamesContextProvider: React.FC<GamesContextProps> = ({
  children,
}) => {
  const [games, updateGameState, addGame, deleteGame] = useGameManager()

  return (
    <GamesContext.Provider
      value={{ games: games, updateGameState, addGame, deleteGame }}
    >
      {children}
    </GamesContext.Provider>
  )
}

export default GamesContext
