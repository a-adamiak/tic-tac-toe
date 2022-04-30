using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Bot.Chain
{
    public interface IBotStrategy
    {
        public IBotStrategy? Successor { get; }
        public (int row, int column)? GetNextMove(Tag?[,] game, Tag nextMove);
    }
}
