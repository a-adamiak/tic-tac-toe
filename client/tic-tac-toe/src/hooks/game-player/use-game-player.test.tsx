import {act, renderHook} from "@testing-library/react-hooks";
import {useGamePlayer} from "./use-game-player";
import {errorBuilder, gameBuilder} from "../../tests/builder";
import {BrowserRouter} from "react-router-dom";
import {randomText} from "../../helpers";
import {mockError, mockResponse, mockIsLoadingCell} from "../../setupTests";
import {GameStatus} from "../../enums";
import {ReactElement} from "react";


describe('Game player hook', () => {
    test('return loaded game', () => {
        // Arrange

        const gameResult = gameBuilder();
        const gameId = randomText();
        mockResponse.mockReturnValue(gameResult);

        // Act

        const { result } = renderHook(() => useGamePlayer(gameId), {wrapper});

        const [_, game] = result.current;

        // Assert

        expect(game).toEqual(gameResult);
    });

    test('when valid can play', () => {

        const errorResult = null;
        const gameResult = {status: GameStatus.InProgress};

        mockError.mockReturnValue(errorResult);
        mockIsLoadingCell.mockReturnValue(false);
        mockResponse.mockReturnValue(gameResult);

        // Arrange
        const id: string = randomText();

        // Act

        const { result } = renderHook(() => useGamePlayer(id), {wrapper});

        const [canPlay] = result.current;

        // Assert

        expect(canPlay).toBe(true);
    });

    test('when error then can not play', () => {

        const errorResult = errorBuilder();
        const gameResult = {status: GameStatus.InProgress};

        mockError.mockReturnValue(errorResult);
        mockIsLoadingCell.mockReturnValue(false);
        mockResponse.mockReturnValue(gameResult);

        // Arrange
        const id: string = randomText();

        // Act

        const { result } = renderHook(() => useGamePlayer(id), {wrapper});

        const [canPlay] = result.current;

        // Assert

        expect(canPlay).toBe(false);
    });

    test('when is loading then can not play', () => {

        const errorResult = null;
        const gameResult = {status: GameStatus.InProgress};

        mockError.mockReturnValue(errorResult);
        mockIsLoadingCell.mockReturnValue(true);
        mockResponse.mockReturnValue(gameResult);

        // Arrange
        const id: string = randomText();

        // Act

        const { result } = renderHook(() => useGamePlayer(id), {wrapper});

        // Assert

        const [canPlay] = result.current;

        expect(canPlay).toBe(false);
    });

    test('should update game on tag', () => {

        // Arrange
        const id: string = randomText();
        const gameResult = gameBuilder();
        const tagResult = gameBuilder();

        mockResponse.mockReturnValue(gameResult);

        const { result } = renderHook(() => useGamePlayer(id), {wrapper});
        const [_, existingGame, tagCell] = result.current;

        mockResponse.mockReturnValue(tagResult);
        // Act

        act(() => {
            tagCell(0,0);
        })

        // Assert

        const [__, game] = result.current;

        expect(game).toEqual(tagResult);
    });
});

const wrapper = (props: {children: ReactElement | ReactElement[]} ) => (
    <>
        <BrowserRouter>
            {props.children}
        </BrowserRouter>
    </>);

