using System;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using FluentAssertions;
using TicTacToe.Domain.Constants;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Exceptions;
using TicTacToe.Domain.Helpers;
using TicTacToe.Tests.Utils;
using Xunit;

namespace TicTacToe.Tests.Domain
{
    public class GameTests
    {
        private readonly IFixture _fixture;

        public GameTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }


        [Fact]
        internal void Delete_WhenDeleted_ThrowsGameNotExist()
        {
            //Arrange

            var game = _fixture.BuildReadonly<Game>()
                .WithOverride(e => e.Status, GameStatus.Deleted)
                .Create();

            // Act

            Action func = () => game.Delete();

            // Assert

            func.Should().ThrowExactly<GameNotExist>();
        }

        [Fact]
        internal void Delete_When_NotNotDeleted_DeleteGame()
        {
            //Arrange

            var status = _fixture
                .Create<Generator<GameStatus>>()
                .FirstOrDefault(s => s != GameStatus.Deleted);

            var game = _fixture.BuildReadonly<Game>()
                .WithOverride(e => e.Status, status)
                .Create();

            // Act

            game.Delete();

            // Assert

            game.Status.Should().Be(GameStatus.Deleted);
        }

        [Theory, AutoData]
        internal void MarkAsFailed_Should_UpdateStatus(Game game)
        {
            //Arrange

            // Act

            game.MarkAsFailed();

            // Assert

            game.Status.Should().Be(GameStatus.Failed);
        }

        [Theory, AutoData]
        internal void MarkCell_WhenNotInProgress_ThrowsGameAlreadyFinished(Tag newTag)
        {
            //Arrange

            var status = _fixture
                .Create<Generator<GameStatus>>()
                .FirstOrDefault(s => s != GameStatus.InProgress);

            var game = _fixture.BuildReadonly<Game>()
                .WithOverride(e => e.Status, status)
                .Create();

            // Act

            Action func = () => game.MarkCell(newTag, CellSizes.RowsLength - 1, CellSizes.ColumnsLength - 1);

            // Assert


            func.Should().ThrowExactly<GameAlreadyFinished>();
        }

        [Theory, AutoData]
        internal void MarkCell_WhenCellAlreadyMarked_ThrowsCellAlreadyMarked(Tag newTag, Guid id, Tag initialTag)
        {
            //Arrange

            var game = new Game(id, GameStatus.InProgress, initialTag)
            {
                Cells = new Tag?[3, 3]
                {
                    { Tag.O, Tag.O, Tag.O },
                    { Tag.O, Tag.O, Tag.O },
                    { Tag.O, Tag.O, Tag.O },
                }
            };

            // Act

            Action func = () => game.MarkCell(newTag, CellSizes.RowsLength - 1, CellSizes.ColumnsLength - 1);

            // Assert


            func.Should().ThrowExactly<CellAlreadyMarked>();
        }

        [Theory, AutoData]
        internal void MarkCell_SameTagRepeated_InvalidTurn(Tag newTag, Guid id, Tag initialTag)
        {
            //Arrange

            var game = new Game(id, GameStatus.InProgress, initialTag)
            {
                Cells = new Tag?[3, 3]
            };

            // Act

            game.MarkCell(newTag, 0, 0);

            Action func = () => game.MarkCell(newTag, CellSizes.RowsLength - 1, CellSizes.ColumnsLength - 1);

            // Assert


            func.Should().ThrowExactly<InvalidTurn>();
        }

        [Theory, AutoData]
        internal void MarkCell_Should_KeepOpenGame(Tag newTag, Guid id, Tag initialTag)
        {
            //Arrange

            var game = new Game(id, GameStatus.InProgress, initialTag)
            {
                Cells = new Tag?[3, 3]
            };

            // Act

            game.MarkCell(newTag, 0, 0);

            // Assert


            game.Status.Should().Be(GameStatus.InProgress);
        }

        [Theory, AutoData]
        internal void MarkCell_WhenXWon_ShouldFinishGame(Guid id, Tag initialTag)
        {
            //Arrange

            var game = new Game(id, GameStatus.InProgress, initialTag)
            {
                Cells = new Tag?[3, 3]
                {
                    { Tag.X, Tag.O, Tag.O },
                    { null, Tag.X, null},
                    { null, null, null },
                }
            };

            // Act

            game.MarkCell(Tag.X, 2, 2);

            // Assert


            game.Status.Should().Be(GameStatus.XWon);
        }

        [Theory, AutoData]
        internal void MarkCell_WhenOWon_ShouldFinishGame(Guid id, Tag initialTag)
        {
            //Arrange

            var game = new Game(id, GameStatus.InProgress, initialTag)
            {
                Cells = new Tag?[3, 3]
                {
                    { null, Tag.O, Tag.O },
                    { null, Tag.X, null},
                    { null, null, null },
                }
            };

            // Act

            game.MarkCell(Tag.O, 0, 0);

            // Assert


            game.Status.Should().Be(GameStatus.OWon);
        }

        [Theory, AutoData]
        internal void MarkCell_WhenNoCells_ShouldDrawGame(Guid id, Tag initialTag)
        {
            //Arrange

            var game = new Game(id, GameStatus.InProgress, initialTag)
            {
                Cells = new Tag?[3, 3]
                {
                    { Tag.X, Tag.O, Tag.O },
                    { Tag.O, Tag.X, Tag.X},
                    { null, Tag.O, Tag.O },
                }
            };

            // Act

            game.MarkCell(Tag.X, 2, 0);

            // Assert


            game.Status.Should().Be(GameStatus.Draw);
        }
    }
}
