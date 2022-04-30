using TicTacToe.Application.Shared;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Application.Queries.GetGame;

public sealed record GetGame(Guid Id) : IQuery<Game>;