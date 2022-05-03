import {build, oneOf, sequence} from "@jackfranklin/test-data-bot";
import {GameStatus, IApiError, IGame, IGameMetadata, Tag} from "../models";
import {randomText} from "../helpers";

export const statusBuilder = build<{status: GameStatus}>({
    fields: {
        status: oneOf<GameStatus>(
            GameStatus.InProgress,
            GameStatus.Loading,
            GameStatus.ClientWon,
            GameStatus.BotWon,
            GameStatus.Draw,
            GameStatus.Failed)
    }
})

export const tagBuilder = build<{tag: Tag}>({
    fields: {
        tag: oneOf<Tag>(Tag.O, Tag.X)
    }
})

export const gameBuilder = build<IGame>({
    fields: {
        id: randomText(),
        status: statusBuilder().status,
        cells: [[null, null, null]],
        clientTag: tagBuilder().tag
    },
})

export const gameMetadataBuilder = build<IGameMetadata>({
    fields: {
        id: randomText(),
        status: statusBuilder().status
    },
})

export const errorBuilder = build<IApiError>({
    fields: {
        message: randomText(),
        code: randomText(),
        status: sequence()
    },
})
