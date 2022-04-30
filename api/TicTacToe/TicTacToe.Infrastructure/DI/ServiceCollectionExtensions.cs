using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Application.Repositories;
using TicTacToe.Application.Shared;
using TicTacToe.Infrastructure.Repositories;
using TicTacToe.Infrastructure.Shared;

namespace TicTacToe.Infrastructure.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();

        services.AddSingleton<IGameRepository, GameRepository>();

        return services;
    }
}