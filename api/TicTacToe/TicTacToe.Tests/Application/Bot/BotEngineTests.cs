using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TicTacToe.Application.Bot;
using TicTacToe.Application.Bot.Chain;
using TicTacToe.Domain.Enums;
using TicTacToe.Tests.Utils;
using Xunit;

namespace TicTacToe.Tests.Application.Bot
{
    public class BotEngineTests
    {
        [Theory, AutoMoqData]
        public void GetNextMove_When_TryWin_Should_MoveToSuccessor(
            Tag?[,] tags,
            Tag botType,
            [Frozen] Mock<ILookForWinStrategy> lookForWinStrategy,
            [Frozen] Mock<IBotStrategy> botStrategy,
            BotEngine engine)
        {
            //Arrange

            lookForWinStrategy.Setup(e => e.GetNextMove(It.IsAny<Tag?[,]>(), It.IsAny<Tag>()))
                .Returns(() => null);
            lookForWinStrategy.Setup(e => e.Successor)
                .Returns(botStrategy.Object);

            botStrategy.Setup(e => e.Successor)
                .Returns(() => null);


            // Act

             engine.GetNextMove(tags, botType, true);

            // Assert

            botStrategy.Verify(e => e.GetNextMove(It.IsAny<Tag?[,]>(), It.IsAny<Tag>()));
        }

        [Theory, AutoMoqData]
        public void GetNextMove_When_TryWin_Should_ReturnResult(
            (int row, int column) cell,
            Tag?[,] tags,
            Tag botType,
            [Frozen] Mock<ILookForWinStrategy> lookForWinStrategy,
                BotEngine engine)
        {
            //Arrange

            lookForWinStrategy.Setup(e => e.GetNextMove(It.IsAny<Tag?[,]>(), It.IsAny<Tag>()))
                .Returns(() => cell);
            
            // Act

            var result = engine.GetNextMove(tags, botType, true);

            // Assert

            result.Should().BeEquivalentTo(cell);
        }

        [Theory, AutoMoqData]
        public void GetNextMove_When_NotTryWin_Should_ReturnResult(
            (int row, int column) cell,
            Tag?[,] tags,
            Tag botType,
            [Frozen] Mock<ILookForOpenSpaceStrategy> lookForOpenSpaceStrategy,
            BotEngine engine)
        {
            //Arrange

            lookForOpenSpaceStrategy.Setup(e => e.GetNextMove(It.IsAny<Tag?[,]>(), It.IsAny<Tag>()))
                .Returns(() => cell);

            // Act

            var result = engine.GetNextMove(tags, botType, false);

            // Assert

            result.Should().BeEquivalentTo(cell);
        }

        [Theory, AutoMoqData]
        public void GetNextMove_When_NoResult_Should_MoveReturnNull(
            Tag?[,] tags,
            Tag botType,
            [Frozen] Mock<ILookForOpenSpaceStrategy> lookForOpenSpaceStrategy,
            BotEngine engine)
        {
            //Arrange

            lookForOpenSpaceStrategy.Setup(e => e.GetNextMove(It.IsAny<Tag?[,]>(), It.IsAny<Tag>()))
                .Returns(() => null);

            lookForOpenSpaceStrategy.Setup(e => e.Successor)
                .Returns(() => null);

            // Act

            var result = engine.GetNextMove(tags, botType, false);

            // Assert

            result.Should().BeNull();
        }
    }
}
