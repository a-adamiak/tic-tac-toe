import {render} from "@testing-library/react";
import {build, oneOf,} from '@jackfranklin/test-data-bot'
import Cell from "../Cell";
import {ICellProps} from "../Cell/Cell";
import {Tag} from "../../enums";
import userEvent from "@testing-library/user-event";

const canPlayCellBuilder = build<ICellProps>({
    fields: {
        canPlay: true,
        cellTagged: tag => {},
        tag: null,
        clientTag: oneOf<Tag>(Tag.X, Tag.O)
    }
})

const canNotPlayCellBuilder = build<ICellProps>({
    fields: {
        canPlay: false,
        cellTagged: tag => {},
        tag: null,
        clientTag: oneOf<Tag>(Tag.X, Tag.O)
    }
})

const alreadyTaggedCellBuilder = build<ICellProps>({
    fields: {
        canPlay: false,
        cellTagged: tag => {},
        tag: oneOf<Tag>(Tag.X, Tag.O),
        clientTag: oneOf<Tag>(Tag.X, Tag.O)
    }
})

describe('Cell component', () => {
    test('when can play could fire cell click', () => {

        // Arrange
        const props = canPlayCellBuilder();
        const cellTaggedSpy = jest.spyOn(props, "cellTagged")

        const {container} = render(
            <Cell tag={props.tag}
                  clientTag={props.clientTag}
                  canPlay={props.canPlay}
                  cellTagged={props.cellTagged}
            />);

        // Act

        const area = container.firstElementChild!;
        userEvent.click(area);

        // Assert

        expect(cellTaggedSpy).toHaveBeenCalledTimes(1);

    })

    test('when can not play could not fire cell click', () => {

        // Arrange

        const props = canNotPlayCellBuilder();
        const cellTaggedSpy = jest.spyOn(props, "cellTagged")

        const {container} = render(
            <Cell tag={props.tag}
                  clientTag={props.clientTag}
                  canPlay={props.canPlay}
                  cellTagged={props.cellTagged}
            />);

        // Act

        const area = container.firstElementChild!;
        userEvent.click(area);

        // Assert

        expect(cellTaggedSpy).not.toHaveBeenCalled();

    })

    test('when already tagged could not fire cell click', () => {

        // Arrange

        const props = alreadyTaggedCellBuilder();
        const cellTaggedSpy = jest.spyOn(props, "cellTagged")

        const {container} = render(
            <Cell tag={props.tag}
                  clientTag={props.clientTag}
                  canPlay={props.canPlay}
                  cellTagged={props.cellTagged}
            />);

        // Act

        const area = container.firstElementChild!;
        userEvent.click(area);

        // Assert

        expect(cellTaggedSpy).not.toHaveBeenCalled();

    })
});