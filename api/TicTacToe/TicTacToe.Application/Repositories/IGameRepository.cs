using TicTacToe.Domain.Entities;

namespace TicTacToe.Application.Repositories;

public interface IGameRepository
{
    public Task<IEnumerable<Game>> GetAllAsync(CancellationToken token = default);

    public Task<Game> GetAsync(Guid id, CancellationToken token = default);

    public Task UpdateAsync(Game entity, CancellationToken token = default);

    public Task CreateAsync(Game entity, CancellationToken token = default);
}