using System.Collections.Generic;
using FluentAssertions;
using TicTacToe.Application.Bot.Chain;
using TicTacToe.Domain.Enums;
using Xunit;

namespace TicTacToe.Tests.Application.Bot
{
    public class LookForCornerStrategyTests
    {
        [Theory, MemberData(nameof(TestData))]
        internal void GetNextMove_Should_ReturnFirstCorner(Tag?[,] game, Tag move, (int row, int column)? expectedResult)
        {
            //Arrange

            // Act

            var result = new LookForCornerStrategy(null).GetNextMove(game, move);
            
            // Assert

            result.Should().BeEquivalentTo(expectedResult);
        }

        public static IEnumerable<object[]> TestData => new List<object[]>
        {
            new object[]
            {
                new Tag?[3,3]
                {
                    {null, null, null},
                    {null, null, null},
                    {null, null, null}
                },
                Tag.X,
                (0,0)
            },
            new object[]
            {
                new Tag?[3,3]
                {
                    {Tag.X, Tag.X, Tag.X},
                    {Tag.X, null, Tag.X},
                    {Tag.X, Tag.X, null}
                },
                Tag.X,
                (2,2)
            },
            new object[]
            {
                new Tag?[3,3]
                {
                    {Tag.X, Tag.X, Tag.X},
                    {Tag.X, Tag.X, Tag.X},
                    {null, Tag.X, null}
                },
                Tag.O,
                (2,0)
            },
            new object[]
            {
                new Tag?[3,3]
                {
                    {Tag.X, Tag.X, Tag.X},
                    {Tag.X, Tag.X, Tag.X},
                    {Tag.O, Tag.X, Tag.O}
                },
                Tag.O,
                null
            },
        };
    }
}
