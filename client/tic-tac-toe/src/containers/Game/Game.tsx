import Board from '../../components/Board'
import { useGamePlayer } from '../../hooks'
import { useParams } from 'react-router-dom'
import styles from './Game.module.scss'
import BoardStatus from '../../components/BoardStatus'

const Game = () => {
  const params = useParams()
  const { gameId } = params

  const [canPlay, game, tagCell] = useGamePlayer(gameId as string)

  return (
    <div className={styles.game}>
      <BoardStatus status={game.status} />
      <Board
        cells={game.cells}
        clientTag={game.clientTag}
        canPlay={canPlay}
        tagCell={tagCell}
      />
    </div>
  )
}

export default Game
