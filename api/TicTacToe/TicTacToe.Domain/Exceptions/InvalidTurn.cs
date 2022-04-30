using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.Exceptions;

public sealed class InvalidTurn : DomainException
{
    public override string Code => nameof(InvalidTurn);

    public InvalidTurn(Tag tag): base($"It is not the {tag} turn")
    {
    }
}