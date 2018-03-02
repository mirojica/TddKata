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

            Action turnOnAction = () => christmasLightsGrid.TurnOn(-1, 0, 5, 6);
            Action turnOffAction = () => christmasLightsGrid.TurnOff(-1, 0, 5, 6);

            turnOnAction.Should().Throw<ArgumentException>();
            turnOffAction.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void CoordinatesShouldNotBeHigherThanMaximum()
        {
            var christmasLightsGrid = new ChristmasLightsGrid();

            Action turnOnAction = () => christmasLightsGrid.TurnOn(1000, 55, 88, 99);
            Action turnOffAction = () => christmasLightsGrid.TurnOff(1000, 55, 88, 99);

            turnOffAction.Should().Throw<ArgumentException>();
            turnOnAction.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(0, 0, 999, 999, 1000000)]
        [InlineData(0, 0, 2, 2, 9)]
        [InlineData(20, 10, 2, 2, 171)]
        [InlineData(13, 100, 13, 150, 51)]
        [InlineData(17, 112, 17, 112, 1)]
        public void TurnOnShouldTurnOnLightsOnProvidedCoordinates_WhenAllLightsAreOff(int xCordinate1, int yCordinate1, int xCordinate2, int yCordinate2, int expectedNumberOfLightsOn)
        {
            var christmasLightsGrid = new ChristmasLightsGrid();

            christmasLightsGrid.TurnOn(xCordinate1, yCordinate1, xCordinate2, yCordinate2);

            var actualLightsOnNumber = christmasLightsGrid.NumberOfLightsOn;
            actualLightsOnNumber.Should().Be(expectedNumberOfLightsOn);
        }

        [Fact]
        public void TurnOnShouldTurnOnLightsOnProvidedCoordinates_WhenThereIsAlreadyTurnedOnLightsAndAreNotIntersected()
        {
            var christmasLightGrid = new ChristmasLightsGrid();
            christmasLightGrid.TurnOn(0, 0, 2, 2);

            christmasLightGrid.TurnOn(4, 4, 6, 6);

            var actualLightsOnNumber = christmasLightGrid.NumberOfLightsOn;
            actualLightsOnNumber.Should().Be(18);
        }

        [Fact]
        public void TurnOnShouldTurnOnLightsOnProvidedCoordinates_WhenThereIsAlreadyTurnedOnLightsAndAreIntersected()
        {
            var christmasLightGrid = new ChristmasLightsGrid();
            christmasLightGrid.TurnOn(7, 14, 3, 12);

            christmasLightGrid.TurnOn(4, 10, 8, 13);

            var actualLightsOnNumber = christmasLightGrid.NumberOfLightsOn;
            actualLightsOnNumber.Should().Be(27);
        }

        [Fact]
        public void TurnOffShouldTurnOffLightsOnProvidedCoordinates_WhenAllLightsAreOn()
        {
            var christmasLightGrid = new ChristmasLightsGrid();
            christmasLightGrid.TurnOn(0, 0, 999, 999);

            christmasLightGrid.TurnOff(0, 0, 2, 2);

            var actualLightsOnNumber = christmasLightGrid.NumberOfLightsOn;
            actualLightsOnNumber.Should().Be(999991);
        }

        [Fact]
        public void TurnOffShouldTurnOffLightsOnProvidedCordinates_WhenThereIsAlreadyTurnedOffLightsAndAreNotIntersected()
        {
            var christmasLightGrid = new ChristmasLightsGrid();
            christmasLightGrid.TurnOn(0, 0, 999, 999);
            christmasLightGrid.TurnOff(0, 0, 2, 2);

            christmasLightGrid.TurnOff(4, 4, 6, 6);

            var actualLightsOnNumber = christmasLightGrid.NumberOfLightsOn;
            actualLightsOnNumber.Should().Be(999982);
        }

        [Fact]
        public void TurnOffShouldTurnOffLightsOnProvidedCordinates_WhenTherreIsAlreadyTurnedOffLightsAndAreIntersected()
        {
            var christmasLightGrid = new ChristmasLightsGrid();
            christmasLightGrid.TurnOn(0, 0, 999, 999);
            christmasLightGrid.TurnOff(3, 3, 5, 5);

            christmasLightGrid.TurnOff(4, 4, 6, 6);

            var actualLightsOnNumber = christmasLightGrid.NumberOfLightsOn;
            actualLightsOnNumber.Should().Be(999986);
        }
    }
}