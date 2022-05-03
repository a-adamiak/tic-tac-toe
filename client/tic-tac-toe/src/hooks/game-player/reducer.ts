import {IGame, Tag} from '../../models'
import { GameAction, GameActionKind } from './actions'

export const gameReducer = (state: IGame, action: GameAction) => {
  switch (action.type) {
    case GameActionKind.SET:
      return action.payload
    case GameActionKind.TAG_CELL:
      return {
        ...state,
        cells: state.cells.map((arr: (Tag | null)[], row: number) =>
          arr.map((tag: Tag | null, column: number) =>
            row === action.payload.row && column === action.payload.column
              ? state.clientTag
              : tag,
          ),
        ),
      }
    default:
      return { ...state }
  }
}
