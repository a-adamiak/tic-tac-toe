import styles from './Cell.module.scss'
import { Tag } from '../../enums'
import { FC, useCallback, useEffect, useState } from 'react'

export interface ICellProps {
  tag: Tag | null
  cellTagged: (tag: Tag) => void
  clientTag: Tag
  canPlay: boolean
}

const Cell: FC<ICellProps> = ({ tag, cellTagged, clientTag, canPlay }) => {
  const [state, setState] = useState<Tag | null>(tag)

  useEffect(() => setState(tag), [tag])

  const clickable = !state && canPlay
  const clickableClass = clickable ? styles['cell--active'] : ''

  const onClick = useCallback(() => {
    if (!clickable) {
      return
    }
    setState(clientTag)
    cellTagged(clientTag)
  }, [setState, state, clickable])

  return (
    <section className={`${styles.cell} ${clickableClass}`} onClick={onClick}>
      {!!tag && <span className={styles.cell__icon}>{tag}</span>}
    </section>
  )
}

export default Cell
