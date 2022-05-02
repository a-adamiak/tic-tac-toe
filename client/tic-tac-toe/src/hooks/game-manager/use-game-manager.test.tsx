import {act, renderHook} from "@testing-library/react-hooks";
import {useGameManager} from "./use-game-manager";
import {gameBuilder} from "../../tests/builder";
import {BrowserRouter} from "react-router-dom";
import {mockResponse} from "../../setupTests";


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

    test('adds game', () => {
        // Arrange

        const initialGames = [0,1].map(() => gameBuilder());
        mockResponse.mockReturnValue(initialGames);

        const { result } = renderHook(() => useGameManager(), {wrapper});

        const [_, __, addGame] = result.current;

        mockResponse.mockReturnValue(gameBuilder());

        // Act

        act(() => {
            addGame();
        })


        // Assert

        const [updatedGames] = result.current;

        expect(updatedGames.length).toEqual(initialGames.length + 1);
    });
});

const wrapper = ({children}) => (
    <>
        <BrowserRouter>
            {children}
        </BrowserRouter>
    </>);

