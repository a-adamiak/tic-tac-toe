import styles from './BoardStatus.module.scss'
import { FC } from 'react'
import {GameStatus} from "../../../models";

export interface IBoardStatusProps {
  status: GameStatus
}

const BoardStatus: FC<IBoardStatusProps> = ({ status }) => {
  const className = `status--${status.toLocaleLowerCase()}`

  return (
    <div className={styles.row}>
      <span className={`${styles.status} ${styles[className]}`}>
        <strong>{status}</strong>
      </span>
    </div>
  )
}

export default BoardStatus
