using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Moq;
using TicTacToe.Application.Commands.CreateGame;
using TicTacToe.Application.Shared;
using TicTacToe.Domain.DomainEvents;
using TicTacToe.Tests.Utils;
using Xunit;

namespace TicTacToe.Tests.Application.Commands;

public class CreateGameHandlerTests
{
    [Theory, AutoMoqData]
    internal async Task HandleAsync_Should_CreateGame(
        CreateGame createGame,
        [Frozen] Mock<IDomainEventDispatcher> domainEventDispatcher,
        CreateGameHandler handler)
    {
        // Arrange
        

        // Act

        await handler.HandleAsync(createGame);

        // Assert
        
        domainEventDispatcher.Verify(e => 
            e.PublishAsync(It.Is<GameCreated>(
            @event => @event.GameId == createGame.Id), It.IsAny<CancellationToken>()));

    }
}