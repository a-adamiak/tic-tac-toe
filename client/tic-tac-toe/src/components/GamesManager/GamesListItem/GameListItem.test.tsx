import {render, screen} from "@testing-library/react";
import {build, oneOf,} from '@jackfranklin/test-data-bot'
import userEvent from "@testing-library/user-event";
import GamesListItem, {IGameListItemProps} from "./GamesListItem";
import {GameStatus, Tag} from "../../../enums";
import {BrowserRouter} from "react-router-dom";
import {randomText} from "../../../helpers";

const gameLisItemPropsBuilder = build<IGameListItemProps>({
    fields: {
        deleteGame: gameId => {},
        id: randomText(),
        status: oneOf<GameStatus>(
            GameStatus.InProgress,
            GameStatus.Loading,
            GameStatus.ClientWon,
            GameStatus.BotWon,
            GameStatus.Draw,
            GameStatus.Failed)
    }
})

describe('Game List component', () => {
    test('should delete game', () => {

        // Arrange
        const props = gameLisItemPropsBuilder();
        const deleteGameSpy = jest.spyOn(props, "deleteGame")

        render(
            <BrowserRouter>
                <GamesListItem
                    deleteGame={props.deleteGame}
                    status={props.status}
                    id={props.id}
                />
            </BrowserRouter>);

        // Act

        const button = screen.getByRole('button');
        userEvent.click(button);

        // Assert

        expect(deleteGameSpy).toHaveBeenCalledTimes(1);

    })
});
