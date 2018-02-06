using System;
using FluentAssertions;
using Xunit;

namespace TddKata.Basic.TicTacToeKata
{
    public class GameTests
    {
        [Fact]
        public void ExceptionShouldBeThrown_WhenSamePlayerTakeMoreThanOneConsecutiveMoves()
        {
            var game = AGame.WithFilledPosition(1, "X");

            Action action = () => game.Move("X", 2);

            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void ExceptionShouldBeThrown_WhenInvalidPlayerIsProvided()
        {
            var game = new Game();

            Action action = () => game.Move("T", 2);

            action.ShouldThrow<ArgumentException>();
        }

        [Theory]
        [InlineData(10)]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(25)]
        public void ExceptionShouldBeThrown_WhenPositionIsLargerThanNineAndLowerThanOne(int positionOutOfRange)
        {
            var game = new Game();

            Action action = () => game.Move("X", positionOutOfRange);

            action.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData("O")]
        public void ExceptionShouldBeThrown_WhenPlayerMakeMoveToFilledPosition(string activePlayer)
        {
            var game = AGame.WithFilledPosition(2, "X");

            Action action = () => game.Move(activePlayer, 2);

            action.ShouldThrow<InvalidOperationException>();
        }

        [Theory]
        [InlineData("X", "X", 4, 5, 6)]
        [InlineData("O", "O", 4, 5, 6)]
        [InlineData("X", "X", 7, 8, 9)]
        [InlineData("O", "O", 7, 8, 9)]
        [InlineData("X", "X", 1, 2, 3)]
        public void PlayerShouldWin_WhenMakeMovesOnOneHorisontalLineOfPositions(string winnerPlayer, 
                                                                            string expectedWinner, 
                                                                            params int[] horisontalWinPositions)
        {
            var game = AGame.WithWinnerPlayer(winnerPlayer, horisontalWinPositions);

            var winner = game.Outcome();

            winner.Should().Be(expectedWinner);
        }

        [Theory]
        [InlineData("X", "X", 1, 4, 7)]
        [InlineData("O", "O", 1, 4, 7)]
        [InlineData("X", "X", 2, 5, 8)]
        [InlineData("O", "O", 2, 5, 8)]
        [InlineData("X", "X", 3, 6, 9)]
        [InlineData("O", "O", 3, 6, 9)]
        public void PlayerShouldWin_WhenMakeMovesOnOneOfVerticalLineOfPositions(string winnerPlayer,
                                                                            string expectedWinner,
                                                                            params int[] verticalWinPositions)
        {
            var game = AGame.WithWinnerPlayer(winnerPlayer, verticalWinPositions);

            var winner = game.Outcome();

            winner.Should().Be(expectedWinner);
        }

        [Theory]
        [InlineData("X", "X", 1, 5, 9)]
        [InlineData("O", "O", 1, 5, 9)]
        [InlineData("X", "X", 3, 5, 7)]
        [InlineData("O", "O", 3, 5, 7)]
        public void PlayerShouldWin_WhenMakeMovesOnOneOfDiagonalLinePositions(string winnerPlayer,
                                                                            string expectedWinner,
                                                                            params int[] diagonalWinPositions)
        {
            var game = AGame.WithWinnerPlayer(winnerPlayer, diagonalWinPositions);

            var winner = game.Outcome();

            winner.Should().Be(expectedWinner);
        }

        [Fact]
        public void GameIsFinishedWithoutWinner_WhenAllPositionsAreTakenAndNoPlayerHasTakenWinLinePositions()
        {
            var game = AGame.WithoutWinner();

            var result = game.Outcome();

            result.Should().Be("egal");
        }

        [Fact]
        public void AskForNextMove_WhenGameIsNotFinished()
        {
            var game = AGame.WhichIsNotFinished();

            var result = game.Outcome();

            result.Should().Be("next");
        }

        private static GameFactory AGame => new GameFactory();
    }
}