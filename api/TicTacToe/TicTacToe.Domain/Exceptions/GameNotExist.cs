namespace TicTacToe.Domain.Exceptions;

public sealed class GameNotExist: DomainException
{
    public override string Code => nameof(GameNotExist);
        
    public GameNotExist(Guid id): base($"Game with id {id} not exit")
    {
    }
}