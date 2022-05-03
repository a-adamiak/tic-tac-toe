export enum GamesActionKind {
  SET = 'SET',
  UPDATE_STATUS = 'UPDATE_STATUS',
  ADD_GAME = 'ADD_GAME',
  DELETE_GAME = 'DELETE_GAME',
}

export interface GamesAction {
  type: GamesActionKind
  payload: any
}
