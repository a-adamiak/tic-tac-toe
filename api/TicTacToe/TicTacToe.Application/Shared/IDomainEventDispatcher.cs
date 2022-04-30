using TicTacToe.Domain.Shared;

namespace TicTacToe.Application.Shared;

public interface IDomainEventDispatcher
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) 
        where TEvent : class, IDomainEvent;
}