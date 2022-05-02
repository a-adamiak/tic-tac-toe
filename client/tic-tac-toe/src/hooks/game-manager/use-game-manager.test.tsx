import {act, renderHook} from "@testing-library/react-hooks";
import {useGameManager} from "./use-game-manager";
import {gameBuilder} from "../../tests/builder";
import {BrowserRouter} from "react-router-dom";
import {mockResponse} from "../../setupTests";
import {ReactElement} from "react";


describe('Game manager hook', () => {
    test('return existing games', () => {
        // Arrange

        const gamesResult = [0,1].map(() => gameBuilder());
        mockResponse.mockReturnValue(gamesResult);

        // Act

        const { result } = renderHook(() => useGameManager(), {wrapper});

        // Assert

        const [games] = result.current;

        expect(games).toEqual(gamesResult);
    });
});

const wrapper = (props: {children: ReactElement | ReactElement[]} ) => (
    <>
        <BrowserRouter>
            {props.children}
        </BrowserRouter>
    </>);

