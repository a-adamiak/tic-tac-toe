using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Helpers;

namespace TicTacToe.Application.Bot.Chain
{
    internal sealed class LookForBlockStrategy: ILookForBlockStrategy
    {
        private readonly ILookForCornerStrategy _lookForCornerStrategy;
        public IBotStrategy? Successor => _lookForCornerStrategy;

        public LookForBlockStrategy(ILookForCornerStrategy lookForCornerStrategy) 
            => _lookForCornerStrategy = lookForCornerStrategy;


        public (int row, int column)? GetNextMove(Tag?[,] game, Tag nextMove) 
            => TicTacToeGame.GetWinningMove(game, nextMove == Tag.O ? Tag.X: Tag.O);
    }

    public interface ILookForBlockStrategy : IBotStrategy
    {

    }

}
