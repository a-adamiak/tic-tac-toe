import React, { useCallback, useContext } from 'react'
import styles from './GameList.module.scss'
import GamesContext from '../../contexts/games/games-context'
import GamesListItem from '../../components/GamesManager/GamesListItem'
import AddButton from '../../components/GamesManager/AddButton'
import { IGameMetadata } from '../../models'

const GamesList = () => {
  const gamesState = useContext(GamesContext)

  const createGame = useCallback(() => {
    gamesState.addGame()
  }, [])

  const deleteGame = useCallback((gameId: string) => {
    gamesState.deleteGame(gameId)
  }, [])

  return (
    <>
      <div className={styles.header}>
        <span>Games</span>
        <AddButton onClick={createGame} />
      </div>

      <ul className={styles.list}>
        {[...gamesState.games].reverse().map((game: IGameMetadata) => (
          <GamesListItem
            key={game.id}
            id={game.id}
            status={game.status}
            deleteGame={deleteGame}
          />
        ))}
      </ul>
    </>
  )
}

export default GamesList
