using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Moq;
using TicTacToe.Application.Commands.DeleteGame;
using TicTacToe.Application.Commands.MarkSpace;
using TicTacToe.Application.Repositories;
using TicTacToe.Application.Shared;
using TicTacToe.Domain.Constants;
using TicTacToe.Domain.DomainEvents;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;
using TicTacToe.Tests.Utils;
using Xunit;

namespace TicTacToe.Tests.Application.Commands;

public class MarkCellHandlerTests
{
    private readonly IFixture _fixture;

    public MarkCellHandlerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory, AutoMoqData]
    internal async Task HandleAsync_Should_UpdateGame(
        DateTime date,
        Guid gameId, 
        Tag tag,
        [Frozen] Mock<IGameRepository> gameRepository,
        [Frozen] Mock<IDomainEventDispatcher> domainEventDispatcher,
        MarkCellHandler handler)
    {
        // Arrange

        var markCell = new MarkCell(gameId, tag, CellSizes.RowsLength - 1, CellSizes.ColumnsLength - 1);

        var inProgressGame = new Game(gameId, GameStatus.InProgress, tag, date)
        {
            Cells = new Tag?[3, 3]
        };

        gameRepository.Setup(e => e.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(inProgressGame);

        // Act

        await handler.HandleAsync(markCell);

        // Assert

        domainEventDispatcher.Verify(e =>
            e.PublishAsync(It.Is<GameUpdated>(
                @event => @event.GameId == markCell.GameId), It.IsAny<CancellationToken>()));

    }

    [Theory, AutoMoqData]
    internal async Task HandleAsync_When_Won_Should_FinishGame(
        Guid gameId,
        Tag tag,
        DateTime date,
        [Frozen] Mock<IGameRepository> gameRepository,
        [Frozen] Mock<IDomainEventDispatcher> domainEventDispatcher,
        MarkCellHandler handler)
    {
        // Arrange

        var markCell = new MarkCell(gameId, tag, CellSizes.RowsLength - 1, CellSizes.ColumnsLength - 1);

        var inProgressGame = new Game(gameId, GameStatus.InProgress, tag, date)
        {
            Cells = new Tag?[3, 3]
            {
                { null, null, null },
                { null, null, null },
                { tag, tag, null },
            }
        };

        gameRepository.Setup(e => e.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(inProgressGame);

        // Act

        await handler.HandleAsync(markCell);

        // Assert

        domainEventDispatcher.Verify(e =>
            e.PublishAsync(It.Is<GameFinished>(
                @event => @event.GameId == markCell.GameId), It.IsAny<CancellationToken>()));

    }
}