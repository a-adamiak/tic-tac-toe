using TicTacToe.Application.Shared;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Application.Queries.GetAllGames;

public sealed record GetAllGames: IQuery<IEnumerable<Game>>;