using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Bot.Chain
{
    internal sealed class LookForCornerStrategy: ILookForCornerStrategy
    {
        private readonly ILookForOpenSpaceStrategy _lookForOpenSpaceStrategy;
        public IBotStrategy Successor => _lookForOpenSpaceStrategy;

        public LookForCornerStrategy(ILookForOpenSpaceStrategy lookForOpenSpaceStrategy) 
            => _lookForOpenSpaceStrategy = lookForOpenSpaceStrategy;


        public (int row, int column)? GetNextMove(Tag?[,] game, Tag nextMove)
        {
            return game switch
            {
                var g when g[0, 0] is null => (0, 0),
                var g when g[0, 2] is null => (0, 2),
                var g when g[2, 0] is null => (2, 0),
                var g when g[2, 2] is null => (2, 2),
                _ => null
            };
        }
    }

    public interface ILookForCornerStrategy: IBotStrategy
    {

    }

}
