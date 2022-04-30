using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Moq;
using TicTacToe.Application.Commands.DeleteGame;
using TicTacToe.Application.Repositories;
using TicTacToe.Application.Shared;
using TicTacToe.Domain.DomainEvents;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;
using TicTacToe.Tests.Utils;
using Xunit;

namespace TicTacToe.Tests.Application.Commands;

public class DeleteGameHandlerTests
{
    private readonly IFixture _fixture;

    public DeleteGameHandlerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory, AutoMoqData]
    internal async Task HandleAsync_Should_DeleteGame(
        DeleteGame deleteGame,
        [Frozen] Mock<IGameRepository> gameRepository,
        [Frozen] Mock<IDomainEventDispatcher> domainEventDispatcher,
        DeleteGameHandler handler)
    {
        // Arrange

        var inProgressGame = _fixture.BuildReadonly<Game>()
            .WithOverride(e => e.Status, GameStatus.InProgress)
            .Create();

        gameRepository.Setup(e => e.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(inProgressGame);

        // Act

        await handler.HandleAsync(deleteGame);

        // Assert

        domainEventDispatcher.Verify(e =>
            e.PublishAsync(It.Is<GameDeleted>(
                @event => @event.GameId == deleteGame.Id), It.IsAny<CancellationToken>()));

    }
}