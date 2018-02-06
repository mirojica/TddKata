using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace TddKata.Basic.TicTacToeKata
{
    public class GameTests
    {
        [Fact]
        public void ExceptionShouldBeThrown_WhenSamePlayerTakeMoreThanOneConsecutiveMoves()
        {
            var game = new Game();
            game.Move("X", 1);

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
            var game = new Game();
            game.Move("X", 2);

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
            var game = GameWinForPlayer(winnerPlayer, horisontalWinPositions);

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
            var game = GameWinForPlayer(winnerPlayer, verticalWinPositions);

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
            var game = GameWinForPlayer(winnerPlayer, diagonalWinPositions);

            var winner = game.Outcome();

            winner.Should().Be(expectedWinner);
        }

        [Fact]
        public void GameIsFinishedWithoutWinner_WhenAllPositionsAreTakenAndNoPlayerHasTakenWinLinePositions()
        {
            var game = GameWithoutWinner();

            var result = game.Outcome();

            result.Should().Be("egal");
        }

        [Fact]
        public void AskForNextMove_WhenGameIsNotFinished()
        {
            var game = GameWhichIsNotFinished();

            var result = game.Outcome();

            result.Should().Be("next");
        }

        private static Game GameWhichIsNotFinished()
        {
            var game = new Game();
            game.Move("X", 1);
            game.Move("O", 2);
            game.Move("X", 3);
            game.Move("O", 4);
            game.Move("X", 6);
            game.Move("O", 7);
            return game;
        }

        private static Game GameWithoutWinner()
        {
            var game = new Game();
            game.Move("X", 1);
            game.Move("O", 2);
            game.Move("X", 3);
            game.Move("O", 4);
            game.Move("X", 6);
            game.Move("O", 7);
            game.Move("X", 8);
            game.Move("O", 9);
            game.Move("X", 5);
            return game;
        }

        private static Game GameWinForPlayer(string winner, int[] verticalWinPositions)
        {
            var allPositions = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var looserPositions = allPositions.Where(position => !verticalWinPositions.Contains(position)).ToList();

            var looser = GetLooserBasedOn(winner);

            var game = new Game();
            game.Move(winner, verticalWinPositions[0]);
            game.Move(looser, looserPositions.First());
            game.Move(winner, verticalWinPositions[1]);
            game.Move(looser, looserPositions.Last());
            game.Move(winner, verticalWinPositions[2]);
            return game;
        }

        private static string GetLooserBasedOn(string winner)
        {
            return winner.Equals("X") ? "O" : "X";
        }
    }
}