using TicTacToe.Domain.Shared;

namespace TicTacToe.Application.Shared;

public interface IDomainEventHandler<in TEvent> where TEvent : class, IDomainEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}