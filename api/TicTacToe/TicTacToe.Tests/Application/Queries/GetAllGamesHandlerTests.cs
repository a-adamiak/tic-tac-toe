using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TicTacToe.Application.Queries.GetAllGames;
using TicTacToe.Application.Repositories;
using TicTacToe.Domain.Entities;
using TicTacToe.Tests.Utils;
using Xunit;

namespace TicTacToe.Tests.Application.Queries;

public class GetAllGamesHandlerTests
{
    [Theory, AutoMoqData]
    internal async Task HandleAsync_Should_ReturnGames(
        GetAllGames getGames,
        IEnumerable<Game> games,
        [Frozen] Mock<IGameRepository> gameRepository,
        GetAllGamesHandler handler)
    {
        // Arrange

        gameRepository.Setup(e => e.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(games);

        // Act

        var result = await handler.HandleAsync(getGames);

        // Assert

        result.Should().BeEquivalentTo(games);

    }
}