using System;
using System.Collections.Generic;

namespace TddKata.Basic.TicTacToeKata
{
    public class Game
    {
        private readonly Board _board = new Board();
        private string _previousPlayer;

        private readonly List<string> _validPlayers = new List<string> {"X", "O"};

        public void Move(string player, int position)
        {
            if (IsNotValidPlayer(player) || SameAsPrevious(player))
            {
                throw new ArgumentException();
            }
            if (_board.IsValidPosition(position))
            {
                throw new ArgumentOutOfRangeException();
            }
            if (_board.IsPositionFilled(position))
            {
                throw new InvalidOperationException();
            }

            _board.FillPosition(position, player);
            _previousPlayer = player;
        }

        public string Outcome()
        {
            return _board.Outcom();
        }

        private bool IsNotValidPlayer(string player)
        {
            return !_validPlayers.Contains(player);
        }

        private bool SameAsPrevious(string player)
        {
            return player.Equals(_previousPlayer);
        }
    }
}