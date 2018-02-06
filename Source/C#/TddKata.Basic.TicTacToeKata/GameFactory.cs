using System.Collections.Generic;
using System.Linq;

namespace TddKata.Basic.TicTacToeKata
{
    public class GameFactory
    {
        private readonly List<int> _allPositions = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public Game WithFilledPosition(int position, string player)
        {
            var game = new Game();
            game.Move(player, position);
            return game;
        }

        public Game WhichIsNotFinished()
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

        public Game WithoutWinner()
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

        public Game WithWinnerPlayer(string winner, int[] winPositions)
        {
            var looserPositions = _allPositions.Where(position => !winPositions.Contains(position)).ToList();

            var looser = GetLooserBasedOn(winner);

            var game = new Game();
            game.Move(winner, winPositions[0]);
            game.Move(looser, looserPositions.First());
            game.Move(winner, winPositions[1]);
            game.Move(looser, looserPositions.Last());
            game.Move(winner, winPositions[2]);
            return game;
        }

        private static string GetLooserBasedOn(string winner)
        {
            return winner.Equals("X") ? "O" : "X";
        }
    }
}