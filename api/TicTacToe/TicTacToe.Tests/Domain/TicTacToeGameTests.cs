using System.Collections.Generic;
using FluentAssertions;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Helpers;
using Xunit;

namespace TicTacToe.Tests.Domain
{
    public class TicTacToeGameTests
    {
        [Theory, MemberData(nameof(WinnerTestData))]
        internal void IsWinner_Should_FindWinner(Tag?[,] game, Tag move, bool expectedResult)
        {
            //Arrange

            // Act

            var result = TicTacToeGame.IsWinner(game, move);

            // Assert

            result.Should().Be(expectedResult);
        }


        public static IEnumerable<object[]> WinnerTestData => new List<object[]>
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
                false
            },
            new object[]
            {
                new Tag?[3,3]
                {
                    {Tag.X, null, null},
                    {null, Tag.X, null},
                    {null, null, Tag.X}
                },
                Tag.X,
                true
            },
            new object[]
            {
                new Tag?[3,3]
                {
                    {Tag.X, null, null},
                    {null, Tag.X, null},
                    {null, null, Tag.X}
                },
                Tag.O,
                false
            },
            new object[]
            {
                new Tag?[3,3]
                {
                    {Tag.X, null, Tag.X},
                    {null, Tag.X, null},
                    {null, Tag.X, null}
                },
                Tag.X,
                false
            },
            new object[]
            {
                new Tag?[3,3]
                {
                    {Tag.X, null, null},
                    {null, Tag.X, null},
                    {null, null, Tag.O}
                },
                Tag.X,
                false
            }
        };
    }
}
