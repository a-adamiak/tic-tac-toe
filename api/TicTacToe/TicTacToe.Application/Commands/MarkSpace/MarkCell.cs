using TicTacToe.Application.Shared;
using TicTacToe.Domain.Constants;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Exceptions;

namespace TicTacToe.Application.Commands.MarkSpace;

public sealed record MarkCell: ICommand
{
    public Guid GameId { get; }
    public Tag Type { get; }
    public int Row { get; }
    public int Column { get; }

    public MarkCell(Guid gameId, Tag type, int row, int column)
    {
        // I treat commands as entry to the domain which ensure the correctness of the domain state

        if (row is < 0 or > CellSizes.RowsLength - 1 || column is < 0  or > CellSizes.ColumnsLength - 1)
            throw new InvalidCell(row, column);

        GameId = gameId;
        Type = type;
        Row = row;
        Column = column;
    }
}