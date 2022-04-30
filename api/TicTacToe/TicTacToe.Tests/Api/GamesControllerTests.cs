using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TicTacToe.Api.Controllers;
using TicTacToe.Api.Dto;
using TicTacToe.Application.Queries.GetAllGames;
using TicTacToe.Application.Queries.GetGame;
using TicTacToe.Application.Shared;
using TicTacToe.Domain.Constants;
using TicTacToe.Domain.Entities;
using TicTacToe.Tests.Utils;
using Xunit;

namespace TicTacToe.Tests.Api
{
    public class GamesControllerTests
    {
        internal class ControllerCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Customize<GamesController>(e => e.OmitAutoProperties());
            }
        }

        [Theory, AutoMoqData(typeof(ControllerCustomization))]
        public async Task GetGames_Should_ReturnOk(
            IEnumerable<Game> games,
            [Frozen] Mock<IQueryDispatcher> queryDispatcher,
            GamesController controller)
        {
            // Arrange

            queryDispatcher.Setup(e => e.QueryAsync(It.IsAny<GetAllGames>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(games);

            // Act

            var result = await controller.GetGames();

            // Assert

            result.Should().BeOfType<OkObjectResult>();
        }

        [Theory, AutoMoqData(typeof(ControllerCustomization))]
        public async Task GetGame_Should_ReturnOk(
            Game game,
            Guid id,
            [Frozen] Mock<IQueryDispatcher> queryDispatcher,
            GamesController controller)
        {
            // Arrange

            queryDispatcher.Setup(e => e.QueryAsync(It.IsAny<GetGame>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(game);

            // Act

            var result = await controller.GetGame(id);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Theory, AutoMoqData(typeof(ControllerCustomization))]
        public async Task CreateGame_Should_ReturnAccepted(
            TagDto initialTag,
            GamesController controller)
        {
            // Arrange
            
            // Act

            var result = await controller.CreateGame(initialTag);

            result.Should().BeOfType<AcceptedAtRouteResult>();
        }

        [Theory, AutoMoqData(typeof(ControllerCustomization))]
        public async Task DeleteGame_Should_ReturnNoContent(
            Guid id,
            GamesController controller)
        {
            // Arrange

            // Act

            var result = await controller.DeleteGame(id);

            result.Should().BeOfType<NoContentResult>();
        }

        [Theory, AutoMoqData(typeof(ControllerCustomization))]
        public async Task MarkCell_Should_ReturnOk(
            Guid id,
            Game game,
            [Frozen] Mock<IQueryDispatcher> queryDispatcher,
            GamesController controller)
        {
            // Arrange

            queryDispatcher.Setup(e => e.QueryAsync(It.IsAny<GetGame>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(game);

            // Act

            var result = await controller.MarkCell(id, CellSizes.RowsLength - 1, CellSizes.ColumnsLength -1);

            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
