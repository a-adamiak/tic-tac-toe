import styles from './Cell.module.scss';
import {Tag} from "../../enums";
import {FC, useCallback, useEffect, useState} from "react";

export interface CellProps {
    tag: Tag | null,
    cellTagged: (tag: Tag) => void
    clientTag: Tag,
    canPlay: boolean
}

const Cell: FC<CellProps> = ({tag, cellTagged, clientTag, canPlay}) => {
    const [state, setState] = useState<Tag | null>(tag);

    useEffect(() => setState(tag), [tag]);

    const clickable = !state && canPlay;

    const onClick = useCallback(() => {
        if(!clickable){
            return;
        }
        setState(clientTag);
        cellTagged(clientTag);
    }, [setState, state, clickable]);

    const clickableClass = clickable ?  styles['cell--active'] : '';

    return <div
        className={`${styles.cell} ${clickableClass}`}
        onClick={onClick}>
        { !!tag && <span className={styles.cell__icon}>{tag}</span>}
    </div>
}

export default Cell;