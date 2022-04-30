using TicTacToe.Application.Repositories;
using TicTacToe.Application.Shared;
using TicTacToe.Domain.DomainEvents;

namespace TicTacToe.Application.Commands.DeleteGame;

internal sealed class DeleteGameHandler: ICommandHandler<DeleteGame>
{
    private readonly IGameRepository _repository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public DeleteGameHandler(IGameRepository repository, IDomainEventDispatcher domainEventDispatcher)
    {
        _repository = repository;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task HandleAsync(DeleteGame command, CancellationToken cancellationToken = default)
    {
        var game = await _repository.GetAsync(command.Id, cancellationToken);

        game.Delete();

        await _repository.UpdateAsync(game);

        await _domainEventDispatcher.PublishAsync(new GameDeleted(command.Id));
    }
}