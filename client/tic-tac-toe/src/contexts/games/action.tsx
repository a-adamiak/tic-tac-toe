export enum GamesActionKind {
    SET = 'SET',
    UPDATE_STATUS = 'UPDATE_STATUS',
}

export interface GamesAction {
    type: GamesActionKind;
    payload: any;
}