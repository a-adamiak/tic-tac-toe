// jest-dom adds custom jest matchers for asserting on DOM nodes.
// allows you to do things like:
// expect(element).toHaveTextContent(/react/i)
// learn more: https://github.com/testing-library/jest-dom
import '@testing-library/jest-dom'

jest.spyOn(window, 'alert').mockImplementation(() => {});

export const mockTagCell = jest.fn();
export const mockResponse = jest.fn();
export const mockIsLoadingCell = jest.fn();
export const mockError = jest.fn();
export const resultedGameMock = () => mockResponse();
export const resultedIsLoadingMock = () => mockIsLoadingCell();
export const resultedErrorMock = () => mockError();
export const mockTagCellHandler = () => {
    mockResponse();
    return mockTagCell;
}

jest.mock("./hooks/use-http", () => ({
    useHttp: () => ([
        mockIsLoadingCell(), mockError(), resultedGameMock(), mockTagCellHandler
    ])
}));