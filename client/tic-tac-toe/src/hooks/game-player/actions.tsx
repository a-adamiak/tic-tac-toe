export enum GameActionKind {
    SET = 'SET',
    MARK_CELL = 'MARK_CELL',
}

export interface GameAction {
    type: GameActionKind;
    payload: any;
}