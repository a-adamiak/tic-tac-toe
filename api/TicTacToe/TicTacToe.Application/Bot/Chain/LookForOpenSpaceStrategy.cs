using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Bot.Chain
{
    internal sealed class LookForOpenSpaceStrategy: ILookForOpenSpaceStrategy
    {
        public IBotStrategy? Successor => null;

        public (int row, int column)? GetNextMove(Tag?[,] game, Tag nextMove)
        {
            var rowsLength = game.GetUpperBound(0) + 1;
            var columnsLength = game.GetUpperBound(1) + 1;
            
            for (var i = 0; i < rowsLength; i++)
            for (var j = 0; j < columnsLength; j++)
            {
                if (game[i, j] is null)
                    return (i, j);
            }

            return null;
        }
    }

    public interface ILookForOpenSpaceStrategy : IBotStrategy
    {

    }

}
