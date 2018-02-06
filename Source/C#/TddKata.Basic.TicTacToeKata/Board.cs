using System.Collections.Generic;
using System.Linq;

namespace TddKata.Basic.TicTacToeKata
{
    public class Board
    {
        private readonly int[][] _winPositionLiness =
        {
            new[] { 1, 2, 3}, new[] { 4, 5, 6}, new[] { 7, 8, 9}, // horisontal lines
            new[] { 1, 4, 7}, new[] { 2, 5, 8}, new[] { 3, 6, 9}, // vertical lines
            new [] { 1, 5, 9}, new [] { 3, 5, 7} // diagonal lines
        };

        private readonly Dictionary<int, string> _board = new Dictionary<int, string>();

        public bool IsPositionFilled(int position)
        {
            return _board.ContainsKey(position);
        }

        public void FillPosition(int position, string player)
        {
            _board.Add(position, player);
        }

        public string Outcom()
        {
            foreach (var winPositionLine in _winPositionLiness)
            {
                var playersInLine = winPositionLine.Where(position => _board.ContainsKey(position))
                    .Select(position => _board[position]).ToList();

                if (playersInLine.Count == 3 && playersInLine.Distinct().Count() == 1)
                {
                    return playersInLine.First();
                }
            }

            return IsFull() ? "egal" : "next";
        }

        public bool IsValidPosition(int position)
        {
            return position > 9 || position < 1;
        }

        private bool IsFull()
        {
            return _board.Count == 9;
        }
    }
}
