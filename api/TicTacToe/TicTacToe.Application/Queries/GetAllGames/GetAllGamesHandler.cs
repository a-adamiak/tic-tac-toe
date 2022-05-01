using TicTacToe.Application.Repositories;
using TicTacToe.Application.Shared;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Application.Queries.GetAllGames;


public sealed class GetAllGamesHandler: IQueryHandler<GetAllGames, IEnumerable<Game>>
{
    private readonly IGameRepository _gameRepository;

    public GetAllGamesHandler(IGameRepository gameRepository) => _gameRepository = gameRepository;

    public async Task<IEnumerable<Game>> HandleAsync(GetAllGames query, CancellationToken cancellationToken = default)
    {
        return await _gameRepository.GetAllAsync(cancellationToken);
    }
}