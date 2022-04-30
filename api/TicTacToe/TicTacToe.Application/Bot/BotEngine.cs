using TicTacToe.Application.Bot.Chain;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Bot;

public sealed class BotEngine: IBotEngine
{
    private readonly ILookForWinStrategy _winStrategy;
    private readonly ILookForOpenSpaceStrategy _openSpaceStrategy;

    public BotEngine(ILookForWinStrategy winStrategy, ILookForOpenSpaceStrategy openSpaceStrategy)
    {
        _winStrategy = winStrategy;
        _openSpaceStrategy = openSpaceStrategy;
    }

    public (int row, int column)? GetNextMove(Tag?[,] cells, Tag botType, bool tryWin)
    {
        IBotStrategy? currentStrategy = tryWin ? _winStrategy : _openSpaceStrategy;

        do
        {
            var nextMove = currentStrategy.GetNextMove(cells, botType);

            if (nextMove is not null)
                return (nextMove.Value.row, nextMove.Value.column);

            currentStrategy = currentStrategy.Successor;
        } 
        while (currentStrategy is not null);
        return null;
    }
}

public interface IBotEngine
{
    (int row, int column)? GetNextMove(Tag?[,] cells, Tag botType, bool tryWin);
}