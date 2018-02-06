using FluentAssertions;
using Xunit;

namespace TddKata.Basic.TicTacToeKata
{
    public class BoardTests
    {
        [Fact]
        public void FillPositionShouldAddFilledPositionWithPlayer()
        {
            var board = new Board();

            board.FillPosition(1, "X");

            var positionIsFilled = board.IsPositionFilled(1);
            positionIsFilled.Should().BeTrue();
        }
    }
}
