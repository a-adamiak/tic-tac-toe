using System.Diagnostics.CodeAnalysis;
using TicTacToe.Application.Bot;
using TicTacToe.Application.Repositories;
using TicTacToe.Application.Shared;
using TicTacToe.Domain.DomainEvents;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.EventHandlers;

/// <summary>
/// Synchronous handler for the bot next move.
/// In the asynchronous game it would be a good example for the sage
/// </summary>
internal sealed class BotGameUpdatedHandler: IDomainEventHandler<GameUpdated>
{
    private readonly IBotEngine _botEngine;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly IGameRepository _gameRepository;

    public BotGameUpdatedHandler(IBotEngine botEngine, IDomainEventDispatcher domainEventDispatcher, 
        IGameRepository gameRepository)
    {
        _botEngine = botEngine;
        _domainEventDispatcher = domainEventDispatcher;
        _gameRepository = gameRepository;
    }

    public async Task HandleAsync(GameUpdated @event, CancellationToken cancellationToken = default)
    {
        var game = await _gameRepository.GetAsync(@event.GameId);
        
        if (ShouldBotMakeNextMove(game, @event.Tag))
        {
            await HandleBotMove(game);
        }
    }

    private Task HandleBotMove(Game game)
    {
        var botTag = game.InitialTag == Tag.O ? Tag.X : Tag.O;
        var tryWin = DateTime.Now.Millisecond % 2 == 0;

        var nextBotMove = _botEngine.GetNextMove(game.Cells, botTag, tryWin);

        return nextBotMove is null ? HandleFailedMove(game) : HandleCorrectMove(game, botTag, nextBotMove);
    }

    private async Task HandleCorrectMove(Game game, Tag botTag, [DisallowNull] (int row, int column)? nextBotMove)
    {
        game.MarkCell(botTag, nextBotMove.Value.row, nextBotMove.Value.column);

        await _gameRepository.UpdateAsync(game);

        // In the different scenario, to protect against infinite loop, we could create a different domain event
        await _domainEventDispatcher.PublishAsync(new GameUpdated(game.Id, botTag));
    }

    private async Task HandleFailedMove(Game game)
    {
        game.MarkAsFailed();

        await _gameRepository.UpdateAsync(game);

        await _domainEventDispatcher.PublishAsync(new GameFailed(game.Id, "Bot couldn't be able to find the next move"));
    }

    private static bool ShouldBotMakeNextMove(Game game, Tag lastMove) 
        => lastMove == game.InitialTag && game.Status == GameStatus.InProgress;
}