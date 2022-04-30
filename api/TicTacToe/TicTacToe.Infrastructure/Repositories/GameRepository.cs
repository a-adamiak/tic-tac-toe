using System.Collections.Concurrent;
using TicTacToe.Application.Repositories;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Exceptions;

namespace TicTacToe.Infrastructure.Repositories;

internal sealed class GameRepository: IGameRepository
{
    private readonly ConcurrentDictionary<Guid, Game> _store = new();
    
    public Task<IEnumerable<Game>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = _store.Values.Where(e => e.Status != GameStatus.Deleted);

        return Task.FromResult(result);
    }

    public Task<Game> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (_store.TryGetValue(id, out var item) == false || item.Status == GameStatus.Deleted)
            throw new GameNotExist(id);

        return Task.FromResult(item);
    }

    public Task UpdateAsync(Game entity, CancellationToken cancellationToken = default)
    {
        if (_store.TryGetValue(entity.Id, out var item) == false || item.Status == GameStatus.Deleted)
            throw new GameNotExist(entity.Id);

        _store[entity.Id] = entity;

        return Task.CompletedTask;
    }

    public Task CreateAsync(Game entity, CancellationToken cancellationToken = default)
    {
        _store[entity.Id] = entity;

        return Task.CompletedTask;
    }
}