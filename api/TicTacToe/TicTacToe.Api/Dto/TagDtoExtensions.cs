using TicTacToe.Domain.Enums;

namespace TicTacToe.Api.Dto
{
    public static class TagDtoExtensions
    {
        public static Tag ToValue(this TagDto type) =>
            type switch
            {
                TagDto.X => Tag.X,
                TagDto.O => Tag.O,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
    }
}
