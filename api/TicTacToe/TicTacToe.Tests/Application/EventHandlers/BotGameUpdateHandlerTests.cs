using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Moq;
using TicTacToe.Application.Bot;
using TicTacToe.Application.EventHandlers;
using TicTacToe.Application.Repositories;
using TicTacToe.Application.Shared;
using TicTacToe.Domain.Constants;
using TicTacToe.Domain.DomainEvents;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;
using TicTacToe.Tests.Utils;
using Xunit;

namespace TicTacToe.Tests.Application.EventHandlers;

public class BotGameUpdateHandlerTests
{
    private readonly IFixture _fixture;

    public BotGameUpdateHandlerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory, AutoMoqData]
    internal async Task HandleAsync_When_NotInProgress_Should_Skip(
        GameUpdated gameUpdated,
        [Frozen] Mock<IGameRepository> gameRepository,
        [Frozen] Mock<IDomainEventDispatcher> domainEventDispatcher,
        BotGameUpdatedHandler handler)
    {
        // Arrange

        var status = _fixture
            .Create<Generator<GameStatus>>()
            .FirstOrDefault(s => GameStatus.InProgress != s);

        var inProgressGame = _fixture.BuildReadonly<Game>()
            .WithOverride(e => e.Status, status)
            .Create();

        gameRepository.Setup(e => e.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(inProgressGame);

        // Act

        await handler.HandleAsync(gameUpdated);

        // Assert

        domainEventDispatcher.Verify(e =>
            e.PublishAsync(It.IsAny<GameUpdated>(), It.IsAny<CancellationToken>()), Times.Never);

        domainEventDispatcher.Verify(e =>
            e.PublishAsync(It.IsAny<GameFailed>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory, AutoMoqData]
    internal async Task HandleAsync_When_NotBotMove_Should_Skip(
        GameUpdated gameUpdated,
        [Frozen] Mock<IGameRepository> gameRepository,
        [Frozen] Mock<IDomainEventDispatcher> domainEventDispatcher,
        BotGameUpdatedHandler handler)
    {
        // Arrange

        var initialTag = _fixture
            .Create<Generator<Tag>>()
            .FirstOrDefault(s => gameUpdated.Tag != s);

        var inProgressGame = _fixture.BuildReadonly<Game>()
            .WithOverride(e => e.InitialTag, initialTag)
            .WithOverride(e => e.Status, GameStatus.InProgress)
            .Create();

        gameRepository.Setup(e => e.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(inProgressGame);

        // Act

        await handler.HandleAsync(gameUpdated);

        // Assert

        domainEventDispatcher.Verify(e =>
            e.PublishAsync(It.IsAny<GameUpdated>(), It.IsAny<CancellationToken>()), Times.Never);

        domainEventDispatcher.Verify(e =>
            e.PublishAsync(It.IsAny<GameFailed>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory, AutoMoqData]
    internal async Task HandleAsync_When_HandleNextMove_And_EngineFailed_Should_FailGame(
        GameUpdated gameUpdated,
        [Frozen] Mock<IGameRepository> gameRepository,
        [Frozen] Mock<IDomainEventDispatcher> domainEventDispatcher,
        [Frozen] Mock<IBotEngine> botEngine,
        BotGameUpdatedHandler handler)
    {
        // Arrange
        var inProgressGame = _fixture.BuildReadonly<Game>()
            .WithOverride(e => e.Status, GameStatus.InProgress)
            .WithOverride(e => e.InitialTag, gameUpdated.Tag)
            .Create();

        gameRepository.Setup(e => e.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(inProgressGame);

        botEngine.Setup(e => e.GetNextMove(It.IsAny<Tag?[,]>(), It.IsAny<Tag>(),
                It.IsAny<bool>()))
            .Returns(() => null);

        // Act

        await handler.HandleAsync(gameUpdated);

        // Assert

        domainEventDispatcher.Verify(e =>
            e.PublishAsync(It.Is<GameFailed>(e => e.GameId == inProgressGame.Id),
                It.IsAny<CancellationToken>()));
    }

    [Theory, AutoMoqData]
    internal async Task HandleAsync_When_HandleNextMove_And_EngineGotMove_Should_UpdateGame(
        Guid gameId,
        Tag initialTag,
        DateTime date,
        GameUpdated gameUpdated,
        [Frozen] Mock<IGameRepository> gameRepository,
        [Frozen] Mock<IDomainEventDispatcher> domainEventDispatcher,
        [Frozen] Mock<IBotEngine> botEngine,
        BotGameUpdatedHandler handler)
    {
        // Arrange

        var inProgressGame = new Game(gameId, GameStatus.InProgress, initialTag, date)
        {
            Cells = new Tag?[3, 3]
        };

        gameRepository.Setup(e => e.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(inProgressGame);

        botEngine.Setup(e => e.GetNextMove(It.IsAny<Tag?[,]>(), It.IsAny<Tag>(),
                It.IsAny<bool>()))
            .Returns(() => new (CellSizes.RowsLength -1 , CellSizes.ColumnsLength - 1));

        // Act

        await handler.HandleAsync(gameUpdated);

        // Assert

        domainEventDispatcher.Verify(e =>
            e.PublishAsync(It.Is<GameUpdated>(f => f.GameId == inProgressGame.Id),
                It.IsAny<CancellationToken>()));
    }
}