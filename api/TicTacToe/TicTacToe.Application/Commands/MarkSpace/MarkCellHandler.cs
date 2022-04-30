using TicTacToe.Application.Repositories;
using TicTacToe.Application.Shared;
using TicTacToe.Domain.DomainEvents;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Commands.MarkSpace;

internal sealed class MarkCellHandler: ICommandHandler<MarkCell>
{
    private readonly IGameRepository _repository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public MarkCellHandler(IGameRepository repository, IDomainEventDispatcher domainEventDispatcher)
    {
        _repository = repository;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task HandleAsync(MarkCell command, CancellationToken cancellationToken = default)
    {
        var game = await _repository.GetAsync(command.GameId, cancellationToken);

        game.MarkCell(command.Type, command.Row, command.Column);

        await _repository.UpdateAsync(game);

        await (game.Status == GameStatus.InProgress
            ? _domainEventDispatcher.PublishAsync(new GameUpdated(game.Id, command.Type))
            : _domainEventDispatcher.PublishAsync(new GameFinished(game.Id)));


    }
}