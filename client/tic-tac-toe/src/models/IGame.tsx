import {GameStatus, Tag} from "../enums";

export interface IGame {
    id: string;
    cells: (Tag | null)[][];
    status: GameStatus;
    clientTag: Tag;
}