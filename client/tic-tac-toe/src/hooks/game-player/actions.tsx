export enum GameActionKind {
  SET = 'SET',
  TAG_CELL = 'TAG_CELL',
}

export interface GameAction {
  type: GameActionKind
  payload: any
}
