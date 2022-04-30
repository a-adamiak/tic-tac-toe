using TicTacToe.Application.Shared;

namespace TicTacToe.Application.Commands.DeleteGame;

public sealed record DeleteGame(Guid Id): ICommand;