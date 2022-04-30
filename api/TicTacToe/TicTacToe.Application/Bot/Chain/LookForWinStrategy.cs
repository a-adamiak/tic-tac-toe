using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Helpers;

namespace TicTacToe.Application.Bot.Chain
{
    internal sealed class LookForWinStrategy: ILookForWinStrategy
    {
        private readonly ILookForBlockStrategy _lookForBlockStrategy;
        public IBotStrategy Successor => _lookForBlockStrategy;

        public LookForWinStrategy(ILookForBlockStrategy lookForBlockStrategy) 
            => _lookForBlockStrategy = lookForBlockStrategy;

        public (int row, int column)? GetNextMove(Tag?[,] game, Tag nextMove) 
            => TicTacToeGame.GetWinningMove(game, nextMove);
    }

    public interface ILookForWinStrategy : IBotStrategy
    {

    }

}
