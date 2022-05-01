using TicTacToe.Application.Repositories;
using TicTacToe.Application.Shared;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Application.Queries.GetGame;

public sealed class GetGameHandler: IQueryHandler<GetGame, Game>
{
    private readonly IGameRepository _gameRepository;

    public GetGameHandler(IGameRepository gameRepository) => _gameRepository = gameRepository;

    public async Task<Game> HandleAsync(GetGame query, CancellationToken cancellationToken = default)
    {
        return await _gameRepository.GetAsync(query.Id, cancellationToken);
    }
}