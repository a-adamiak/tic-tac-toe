namespace TicTacToe.Domain.Exceptions;

public sealed class GameAlreadyFinished: DomainException
{
    public override string Code => nameof(GameAlreadyFinished);
        
    public GameAlreadyFinished(Guid id): base($"Game with id {id} already finished")
    {
    }
}