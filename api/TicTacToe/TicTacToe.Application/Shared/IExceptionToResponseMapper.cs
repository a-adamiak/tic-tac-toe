namespace TicTacToe.Application.Shared;

public interface IExceptionToResponseMapper
{
    ExceptionResponse Map(Exception exception);
}