import {render, screen} from "@testing-library/react";
import {build } from '@jackfranklin/test-data-bot'
import userEvent from "@testing-library/user-event";
import GamesListItem, {IGameListItemProps} from "./GamesListItem";
import {BrowserRouter} from "react-router-dom";
import {randomText} from "../../../helpers";
import {statusBuilder} from "../../../tests/builder";

const gameLisItemPropsBuilder = build<IGameListItemProps>({
    fields: {
        deleteGame: gameId => {},
        id: randomText(),
        status: statusBuilder().status
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
