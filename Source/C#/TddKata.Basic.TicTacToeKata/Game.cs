using System;
using System.Collections.Generic;
using System.Linq;

namespace TddKata.Basic.TicTacToeKata
{
    public class Game
    {
        private readonly Dictionary<int, string> _board = new Dictionary<int, string>();
        private string _previousPlayer;

        private readonly int[][] _winPositions =
        {
            new[] { 1, 2, 3}, new[] { 4, 5, 6}, new[] { 7, 8, 9}, // horisontal lines
            new[] { 1, 4, 7}, new[] { 2, 5, 8}, new[] { 3, 6, 9}, // vertical lines
            new [] { 1, 5, 9}, new [] { 3, 5, 7} // diagonal lines
        };

        private readonly List<string> _validPlayers = new List<string> {"X", "O"};

        public void Move(string player, int position)
        {
            if (!_validPlayers.Contains(player))
            {
                throw new ArgumentException();
            }
            if (position > 9 || position < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (player.Equals(_previousPlayer) || _board.ContainsKey(position))
            {
                throw new InvalidOperationException();
            }

            _board.Add(position, player);
            _previousPlayer = player;
        }

        public string Outcome()
        {
            foreach (var winPosition in _winPositions)
            {
                var playersInLine = winPosition.Where(position => _board.ContainsKey(position))
                    .Select(position => _board[position]).ToList();

                if (playersInLine.Count == 3 && playersInLine.Distinct().Count() == 1)
                {
                    return playersInLine.First();
                }
            }

            return _board.Count == 9 ? "egal" : "next";
        }
    }
}