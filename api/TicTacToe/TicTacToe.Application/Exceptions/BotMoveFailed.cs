namespace TicTacToe.Application.Exceptions;

public sealed class BotMoveFailed: AppException
{
    public override string Code => nameof(BotMoveFailed);
        
    public BotMoveFailed(): base("Bot failed with the next move")
    {
    }
}