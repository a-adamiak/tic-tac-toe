using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Application.Bot;
using TicTacToe.Application.Bot.Chain;
using TicTacToe.Application.Shared;

namespace TicTacToe.Application.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.Scan(s => s.FromAssemblies(Assembly.GetAssembly(typeof(ServiceCollectionExtensions)))
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(s => s.FromAssemblies(Assembly.GetAssembly(typeof(ServiceCollectionExtensions)))
            .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(s => s.FromAssemblies(Assembly.GetAssembly(typeof(ServiceCollectionExtensions)))
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddSingleton<IBotEngine, BotEngine>();
        services.AddSingleton<ILookForCornerStrategy, LookForCornerStrategy>();
        services.AddSingleton<ILookForOpenSpaceStrategy, LookForOpenSpaceStrategy>();
        services.AddSingleton<ILookForBlockStrategy, LookForBlockStrategy>();
        services.AddSingleton<ILookForWinStrategy, LookForWinStrategy>();

        return services;
    }
}