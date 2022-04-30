using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Api.Dto
{
    public static class GameExtensions
    {
        public static GameDto ToDto(this Game entity) => 
            new (entity.Id, entity.Cells.ToDto(), entity.Status.ToDto(), entity.InitialTag.ToDto());

        public static IEnumerable<GameDto> ToDto(this IEnumerable<Game> entities) => entities.Select(ToDto);

        private static TagDto?[][] ToDto(this Tag?[,] cells)
        {
            var rowsLength = cells.GetUpperBound(0) + 1;
            var columnsLength = cells.GetUpperBound(1) + 1;

            var result = new TagDto?[rowsLength][];

            for (var i = 0; i < rowsLength; i++)
            {
                result[i] = new TagDto?[columnsLength];
                for (var j = 0; j < columnsLength; j++)
                    result[i][j] = cells[i, j].ToDto();
            }

            return result;
        }

        private static TagDto ToDto(this Tag type) => ((Tag?)type).ToDto()!.Value;

        private static TagDto? ToDto(this Tag? type) =>
            type switch
            {
                null => null,
                Tag.X => TagDto.X,
                Tag.O => TagDto.O,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

        private static GameStatusDto ToDto(this GameStatus status) =>
            status switch
            {
                GameStatus.InProgress => GameStatusDto.InProgress,
                GameStatus.XWon => GameStatusDto.ClientWon,
                GameStatus.OWon => GameStatusDto.BotWon,
                GameStatus.Draw => GameStatusDto.Draw,
                GameStatus.Failed => GameStatusDto.Failed,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
    }
}
