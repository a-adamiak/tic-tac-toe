import {IGame} from "../../models";
import {FC} from "react";
import {Tag} from "../../enums";

export interface IBoardProps extends IGame {
    canPlay: boolean;
    tagCell: (row: number, column: number) => void
}

const Board: FC<IBoardProps> = ({id, cells, status, clientTag, canPlay}) => {
    return <p>Board works</p>
}

export default Board;