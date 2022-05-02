import {render} from "@testing-library/react";
import {build} from '@jackfranklin/test-data-bot'
import Board, {IBoardProps} from "./Board";
import userEvent from "@testing-library/user-event";
import {tagBuilder} from "../../../tests/builder";

const boardCanPlayPropsBuilder = build<IBoardProps>({
    fields: {
        canPlay: true,
        clientTag: tagBuilder().tag,
        tagCell: (row, column) => {},
        cells: [[null, null, null], [null, null, null], [null, null, null]]
    }
})

const boardCanNotPlayPropsBuilder = build<IBoardProps>({
    fields: {
        canPlay: false,
        clientTag: tagBuilder().tag,
        tagCell: (row, column) => {},
        cells: [[null, null, null], [null, null, null], [null, null, null]]
    }
})

describe('Board component', () => {
    test('show correct number of cells', async () => {

        // Arrange
        const props = boardCanPlayPropsBuilder();

        const {container} = render(<Board
            clientTag={props.clientTag}
            cells={props.cells}
            canPlay={props.canPlay}
            tagCell={props.tagCell}
        />);

        // Act

        // Assert

        expect(container.firstElementChild!.childElementCount).toBe(9);
    })

    test('when can play could tag cell', async () => {

        // Arrange
        const props = boardCanPlayPropsBuilder();
        const tagCellSpy = jest.spyOn(props, "tagCell")

        const {container} = render(<Board
            clientTag={props.clientTag}
            cells={props.cells}
            canPlay={props.canPlay}
            tagCell={props.tagCell}
        />);

        // Act

        // need setup to work
        const area = container.querySelector('section');
        userEvent.click(area!);

        // Assert

        expect(tagCellSpy).toHaveBeenCalledTimes(1);
    })

    test('when can not play could not tag cell', async () => {

        // Arrange
        const props = boardCanNotPlayPropsBuilder();
        const tagCellSpy = jest.spyOn(props, "tagCell")

        const {container} = render(<Board
            clientTag={props.clientTag}
            cells={props.cells}
            canPlay={props.canPlay}
            tagCell={props.tagCell}
        />);

        // Act

        // need setup to work
        const area = container.querySelector('section');
        userEvent.click(area!);

        // Assert

        expect(tagCellSpy).not.toHaveBeenCalled();
    })

    test('should tag cell only once', async () => {

        // Arrange
        const props = boardCanPlayPropsBuilder();
        const tagCellSpy = jest.spyOn(props, "tagCell")

        const {container} = render(<Board
            clientTag={props.clientTag}
            cells={props.cells}
            canPlay={props.canPlay}
            tagCell={props.tagCell}
        />);

        // Act

        // need setup to work
        const area = container.querySelector('section');
        userEvent.click(area!);
        userEvent.click(area!);

        // Assert

        expect(tagCellSpy).toHaveBeenCalledTimes(1);
    })
});