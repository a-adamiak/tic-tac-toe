import {build, oneOf} from "@jackfranklin/test-data-bot";
import {GamesState} from "./state";
import {IGameMetadata} from "../../models";
import {GameStatus} from "../../enums";
import {GamesActionKind} from "./actions";
import {gamesReducer} from "./reducer";
import {randomText} from "../../helpers";

const statusBuilder = build<{status: GameStatus}>({
    fields: {
        status: oneOf<GameStatus>(
            GameStatus.InProgress,
            GameStatus.Loading,
            GameStatus.ClientWon,
            GameStatus.BotWon,
            GameStatus.Draw,
            GameStatus.Failed)
    }
})

const gamesBuilder = build<IGameMetadata>({
    fields: {
        id: randomText(),
        status: statusBuilder().status
    },
})

const stateBuilder = build<GamesState>({
    fields: {
        games: [0].map( _ => gamesBuilder())
    }
})

describe('Game manager reducer', () => {
    test('when set should overwritte games', () => {
        // Arrange

        const initialState = stateBuilder();
        const newGames = [0,1,2,3].map( _ => gamesBuilder())

        // Act

        const result = gamesReducer(initialState, {type: GamesActionKind.SET, payload: newGames});

        // Assert

        expect(result.games).toEqual(newGames);
    })

    test('when delete should delete game', () => {
        // Arrange

        const initialState = stateBuilder();
        const id = initialState.games[0].id;

        // Act

        const result = gamesReducer(initialState, {type: GamesActionKind.DELETE_GAME,
            payload: {gameId: id}});

        // Assert

        expect(result.games.length).toEqual(initialState.games.length - 1);
    })

    test('when update status, status should be changed', () => {
        // Arrange

        const initialState = stateBuilder();
        const id = initialState.games[0].id;
        const newStatus = statusBuilder().status

        // Act

        const result = gamesReducer(initialState, {type: GamesActionKind.UPDATE_STATUS,
            payload: {id, status: newStatus}});

        // Assert

        expect(result.games[0].status).toEqual(newStatus);
    })
});