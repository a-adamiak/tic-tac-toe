import {render} from "@testing-library/react";
import {build } from '@jackfranklin/test-data-bot'
import Cell from "../Cell";
import {ICellProps} from "../Cell/Cell";
import userEvent from "@testing-library/user-event";
import {tagBuilder} from "../../../tests/builder";

const canPlayCellBuilder = build<ICellProps>({
    fields: {
        canPlay: true,
        cellTagged: tag => {},
        tag: null,
        clientTag: tagBuilder().tag
    }
})

const canNotPlayCellBuilder = build<ICellProps>({
    fields: {
        canPlay: false,
        cellTagged: tag => {},
        tag: null,
        clientTag: tagBuilder().tag
    }
})

const alreadyTaggedCellBuilder = build<ICellProps>({
    fields: {
        canPlay: false,
        cellTagged: tag => {},
        tag: tagBuilder().tag,
        clientTag: tagBuilder().tag
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