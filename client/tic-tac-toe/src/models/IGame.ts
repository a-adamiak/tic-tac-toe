import {Tag} from "./Tag";
import {GameStatus} from "./GameStatus";

export interface IGame {
  id: string
  cells: (Tag | null)[][]
  status: GameStatus
  clientTag: Tag
}
