import {build} from "@jackfranklin/test-data-bot";
import {GamesState} from "./state";
import {GamesActionKind} from "./actions";
import {gamesReducer} from "./reducer";
import {gameMetadataBuilder, statusBuilder} from "../../tests/builder";



const stateBuilder = build<GamesState>({
    fields: {
        games: [0].map( _ => gameMetadataBuilder())
    }
})

describe('Game manager reducer', () => {
    test('when set should overwritte games', () => {
        // Arrange

        const initialState = stateBuilder();
        const newGames = [0,1,2,3].map( _ => gameMetadataBuilder())

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