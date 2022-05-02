import {render, screen} from "@testing-library/react";
import {build, oneOf,} from '@jackfranklin/test-data-bot'
import {GameStatus} from "../../enums";
import BoardStatus, {IBoardStatusProps} from "../BoardStatus/BoardStatus";

const boardStatusPropsBuilder = build<IBoardStatusProps>({
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

describe('Board status component', () => {
    test('show correct styling', async () => {

        // Arrange
        const props = boardStatusPropsBuilder();

        render(<BoardStatus status={props.status}/>);

        // Act

        // Assert

        const status = await screen.findByText(props.status);
        expect(status).not.toBeNull();

    })
});