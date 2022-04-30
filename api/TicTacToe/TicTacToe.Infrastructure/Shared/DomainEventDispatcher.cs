using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Application.Shared;
using TicTacToe.Domain.Shared;

namespace TicTacToe.Infrastructure.Shared;

internal sealed class DomainEventDispatcher: IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) 
        where TEvent : class, IDomainEvent
    {
        // we could add interceptor for logging

        using var scope = _serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IDomainEventHandler<TEvent>>();
        var tasks = handlers.Select(x => x.HandleAsync(@event, cancellationToken));

        await Task.WhenAll(tasks);
    }
}