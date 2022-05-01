namespace TicTacToe.Domain.Exceptions;

public sealed class GameNotExist: DomainException
{
    public override string Code => nameof(GameNotExist);
        
    public GameNotExist(string id): base($"Game with id {id} not exit")
    {
    }
}