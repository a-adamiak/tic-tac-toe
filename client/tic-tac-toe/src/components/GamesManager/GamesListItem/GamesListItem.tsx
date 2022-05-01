import { NavLink } from 'react-router-dom'
import React, { FC, useCallback } from 'react'
import styles from './GameListItem.module.scss'
import DeleteButton from '../DeleteButton'
import { IGameMetadata } from '../../../models'

export interface IGameListItemProps extends IGameMetadata {
  deleteGame: (gameId: string) => void
}

const GamesListItem: FC<IGameListItemProps> = ({ id, status, deleteGame }) => {
  const onDeleteGame = useCallback(() => {
    deleteGame(id)
  }, [id, deleteGame])

  return (
    <li className={styles.item} key={id}>
      <NavLink
        className={(navData) =>
          navData.isActive
            ? `${styles.link} ${styles['link--active']}`
            : styles.link
        }
        to={`${id}`}
      >
        {id}
      </NavLink>
      <DeleteButton onClick={onDeleteGame}></DeleteButton>
    </li>
  )
}

export default GamesListItem
