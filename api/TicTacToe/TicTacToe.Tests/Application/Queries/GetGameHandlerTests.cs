using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TicTacToe.Application.Queries.GetGame;
using TicTacToe.Application.Repositories;
using TicTacToe.Domain.Entities;
using TicTacToe.Tests.Utils;
using Xunit;

namespace TicTacToe.Tests.Application.Queries;

public class GetGameHandlerTests
{
    [Theory, AutoMoqData]
    internal async Task HandleAsync_Should_ReturnGame(
        GetGame getGame,
        Game game,
        [Frozen] Mock<IGameRepository> gameRepository,
        GetGameHandler handler)
    {
        // Arrange

        gameRepository.Setup(e => e.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(game);

        // Act

        var result = await handler.HandleAsync(getGame);

        // Assert

        result.Should().BeEquivalentTo(game);

    }
}