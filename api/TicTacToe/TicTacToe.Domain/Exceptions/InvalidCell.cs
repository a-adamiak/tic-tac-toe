namespace TicTacToe.Domain.Exceptions;

public sealed class InvalidCell: DomainException
{
    public override string Code => nameof(InvalidCell);

    public InvalidCell(int row, int column): base($"[{row}][{column}] is a invalid cell.")
    {
    }
}