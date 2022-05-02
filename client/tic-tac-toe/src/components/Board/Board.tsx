import { FC, useEffect, useState } from 'react'
import styles from './Board.module.scss'
import Cell from '../Cell'
import { Tag } from '../../enums'

export interface IBoardProps {
  canPlay: boolean
  tagCell: (row: number, column: number, tag: Tag) => void
  cells: (Tag | null)[][]
  clientTag: Tag
}

const Board: FC<IBoardProps> = ({ cells, clientTag, canPlay, tagCell }) => {
  const [canPlayState, setCanPlay] = useState<boolean>(canPlay)

  useEffect(() => setCanPlay(canPlay), [canPlay])

  return (
    <div className={styles.board}>
      {cells.map((row, rowIndex) =>
        row.map((cell, columnIndex) => (
          <div
              key={`${rowIndex}${columnIndex}`}
              className={styles.board__cell}>
            <Cell
              canPlay={canPlayState}
              clientTag={clientTag}
              tag={cell}
              cellTagged={(tag: Tag) => {
                setCanPlay(false)
                tagCell(rowIndex, columnIndex, tag)
              }}
            />
          </div>
        )),
      )}
    </div>
  )
}

export default Board
