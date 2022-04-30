using System.Collections.Generic;
using FluentAssertions;
using TicTacToe.Application.Bot.Chain;
using TicTacToe.Domain.Enums;
using Xunit;

namespace TicTacToe.Tests.Application.Bot
{
    public class LookForBlockStrategyTests
    {
        [Theory, MemberData(nameof(TestData))]
        internal void GetNextMove_Should_ReturnFirstBlock(Tag?[,] game, Tag move, (int row, int column)? expectedResult)
        {
            //Arrange

            // Act

            var result = new LookForBlockStrategy(null).GetNextMove(game, move);
            
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
                null
            },
            new object[]
            {
                new Tag?[3,3]
                {
                    {Tag.O, Tag.O, Tag.X},
                    {Tag.X, null, null},
                    {Tag.X, Tag.X, null}
                },
                Tag.X,
                null
            },
            new object[]
            {
                new Tag?[3,3]
                {
                    {Tag.O, Tag.O, Tag.X},
                    {Tag.X, null, null},
                    {Tag.X, Tag.X, null}
                },
                Tag.O,
                (2,2)
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
            }
        };
    }
}
