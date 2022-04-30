using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.Exceptions;

public sealed class CellAlreadyMarked: DomainException
{
    public override string Code => nameof(CellAlreadyMarked);

    public CellAlreadyMarked(Guid gameId, int row, int cell, Tag type)
        : base($"Cell [{row}][{cell}] in the game {gameId} is already marked by {type}")
    {
    }
}