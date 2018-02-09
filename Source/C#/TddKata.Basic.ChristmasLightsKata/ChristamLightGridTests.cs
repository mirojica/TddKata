using System.Runtime.InteropServices;
using FluentAssertions;
using Xunit;

namespace TddKata.Basic.ChristmasLightsKata
{
    public class ChristamLightGridTests
    {
        [Fact]
        public void TheLightsAllStartTurnedOff()
        {
            var christmasLightGrid = new ChristmasLightsGrid();

            var actualLightsOnNumber = christmasLightGrid.NumberOfLightsOn;

            actualLightsOnNumber.Should().Be(0);
        }

        [Fact]
        public void ToggleShouldTurnOnAllOffLightsAndAllTurnOffAllOnLights()
        {
            var christmasLightsGrid = new ChristmasLightsGrid();

            christmasLightsGrid.Toggle();

            var actualLightsOnNumber = christmasLightsGrid.NumberOfLightsOn;
            actualLightsOnNumber.Should().Be(1000000);
        }

        [Theory]
        [InlineData(0, 0, 999, 999, 1000000)]
        [InlineData(0, 0, 2, 2, 9)]
        [InlineData(20, 10, 2, 2, 171)]
        [InlineData(13, 100, 13, 150, 51)]
        [InlineData(17, 112, 17, 112, 1)]
        public void TurnOnShouldTurnOnLightsOnProvidedCordinates_WhenAllLightsAreOff(int xCordinate1, int yCordinate1, int xCordinate2, int yCordinate2, int expectedNumberOfLightsOn)
        {
            var christmasLightsGrid = new ChristmasLightsGrid();

            christmasLightsGrid.TurnOn(xCordinate1, yCordinate1, xCordinate2, yCordinate2);

            var actualLightsOnNumber = christmasLightsGrid.NumberOfLightsOn;
            actualLightsOnNumber.Should().Be(expectedNumberOfLightsOn);
        }
    }

    public class ChristmasLightsGrid
    {
        private int _numberOfTurnedOnLights;

        public int NumberOfLightsOn => _numberOfTurnedOnLights;

        public void TurnOn(int xCordinate1, int yCordinate1, int xCordinate2, int yCordinate2)
        {
            var xLine = xCordinate1 > xCordinate2 ? xCordinate1 - xCordinate2 + 1 : xCordinate2 - xCordinate1 + 1;

            var yLine = yCordinate1 > yCordinate2 ? yCordinate1 - yCordinate2 + 1 : yCordinate2 - yCordinate1 + 1;

            _numberOfTurnedOnLights = xLine * yLine;
        }

        public void Toggle()
        {
            _numberOfTurnedOnLights = 1000000;
        }
    }
}