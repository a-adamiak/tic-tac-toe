import {render, screen} from "@testing-library/react";
import {build } from '@jackfranklin/test-data-bot'
import BoardStatus, {IBoardStatusProps} from "../BoardStatus/BoardStatus";
import {statusBuilder} from "../../../tests/builder";

const boardStatusPropsBuilder = build<IBoardStatusProps>({
    fields: {
        status: statusBuilder().status
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