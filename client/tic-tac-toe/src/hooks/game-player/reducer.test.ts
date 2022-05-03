import {GameActionKind} from "./actions";
import {gameReducer} from "./reducer";
import {gameBuilder} from "../../tests/builder";


describe('Game player reducer', () => {
    test('when set should overwritte game', () => {
        // Arrange

        const initialState = gameBuilder();
        const newGame = gameBuilder();

        // Act

        const result = gameReducer(initialState, {type: GameActionKind.SET, payload: newGame});

        // Assert

        expect(result).toEqual(newGame);
    })

    test('when tag cell should update cell tag', () => {
        // Arrange

        const initialState = gameBuilder();

        // Act

        const result = gameReducer(initialState, {type: GameActionKind.TAG_CELL,
            payload: {row: 0, column: 0}});

        // Assert

        expect(result.cells[0][0]).toEqual(initialState.clientTag);
    })
});

