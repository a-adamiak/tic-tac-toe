using TicTacToe.Application.Repositories;
using TicTacToe.Application.Shared;
using TicTacToe.Domain.DomainEvents;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Application.Commands.CreateGame;

internal sealed class CreateGameHandler: ICommandHandler<CreateGame>
{
    private readonly IGameRepository _repository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public CreateGameHandler(IGameRepository repository, IDomainEventDispatcher domainEventDispatcher)
    {
        _repository = repository;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task HandleAsync(CreateGame command, CancellationToken cancellationToken = default)
    {
        var newGame = Game.Create(command.Id, command.FirstTag);

        await _repository.CreateAsync(newGame);

        await _domainEventDispatcher.PublishAsync(new GameCreated(newGame.Id));
    }
}