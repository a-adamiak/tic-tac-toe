import {build, oneOf} from "@jackfranklin/test-data-bot";
import {IGame} from "../../models";
import {GameStatus, Tag} from "../../enums";
import {GameActionKind} from "./actions";
import {gameReducer} from "./reducer";
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

const tagBuilder = build<{tag: Tag}>({
    fields: {
        tag: oneOf<Tag>(Tag.O, Tag.X)
    }
})

const gameBuilder = build<IGame>({
    fields: {
        id: randomText(),
        status: statusBuilder().status,
        cells: [[null, null, null]],
        clientTag: tagBuilder().tag
    },
})



describe('Game manager reducer', () => {
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
        const newTag = tagBuilder().tag;

        // Act

        const result = gameReducer(initialState, {type: GameActionKind.TAG_CELL,
            payload: {row: 0, column: 0, newTag}});

        // Assert

        expect(result.cells[0][0]).toEqual(newTag);
    })
});