using TicTacToe.Application.Shared;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Commands.CreateGame;

public sealed record CreateGame(Guid Id, Tag FirstTag): ICommand;