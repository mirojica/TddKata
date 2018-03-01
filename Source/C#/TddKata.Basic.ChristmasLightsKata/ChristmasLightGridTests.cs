using System;
using FluentAssertions;
using Xunit;

namespace TddKata.Basic.ChristmasLightsKata
{
    public class ChristmasLightGridTests
    {
        [Fact]
        public void TheLightsAllStartsTurnedOff()
        {
            var christmasLightGrid = new ChristmasLightsGrid();

            var actualLightsOnNumber = christmasLightGrid.NumberOfLightsOn;

            actualLightsOnNumber.Should().Be(0);
        }

        [Fact]
        public void ToggleShouldTurnOnAllLights_WhenAllLightsAreOff()
        {
            var christmasLightsGrid = new ChristmasLightsGrid();

            christmasLightsGrid.Toggle();

            var actualLightsOnNumber = christmasLightsGrid.NumberOfLightsOn;
            actualLightsOnNumber.Should().Be(1000000);
        }

        [Fact]
        public void CordinatesShouldNotBeLowerThanMinimum()
        {
            var christmasLightsGrid = new ChristmasLightsGrid();

            Action action = () => christmasLightsGrid.TurnOn(-1, 0, 5, 6);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void CoordinatesShouldNotBeHigherThanMaximum()
        {
            var christmasLightsGrid = new ChristmasLightsGrid();

            Action action = () => christmasLightsGrid.TurnOn(1000, 55, 88, 99);

            action.Should().Throw<ArgumentException>();
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

        [Fact]
        public void TurnOnShouldTurnOnLightsOnProvidedCordinates_WhenThereIsAlreadyTurnedOnLightsAndAreNotIntersected()
        {
            var christmasLightGrid = new ChristmasLightsGrid();
            christmasLightGrid.TurnOn(0, 0, 2, 2);

            christmasLightGrid.TurnOn(4, 4, 6, 6);

            var actualLightsOnNumber = christmasLightGrid.NumberOfLightsOn;
            actualLightsOnNumber.Should().Be(18);
        }

        [Fact]
        public void TurnOnShouldTurnOnLightsOnProvidedCordinates_WhenThereIsAlreadyTurnedOnLightsAndAreIntersected()
        {
            var christmasLightGrid = new ChristmasLightsGrid();
            christmasLightGrid.TurnOn(7, 14, 3, 12);

            christmasLightGrid.TurnOn(4, 10, 8, 13);

            var actualLightsOnNumber = christmasLightGrid.NumberOfLightsOn;
            actualLightsOnNumber.Should().Be(27);
        }
    }
}