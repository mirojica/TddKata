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
            var christmasLightsGrid = new ChristmasLightsGrid();

            christmasLightsGrid.NumberOfLightsOn.Should().Be(0);
        }

        [Fact]
        public void ToggleShouldTurnOnAllLights_WhenAllLightsAreOff()
        {
            var christmasLightsGrid = new ChristmasLightsGrid();

            christmasLightsGrid.Toggle();

            christmasLightsGrid.NumberOfLightsOn.Should().Be(1000000);
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
        public void TurnOnShouldTurnOnLightsOnProvidedCoordinates_WhenAllLightsAreOff(int cornerAXCordinate, int cornerAYCordinate, int cornerBXCordinate, int cornerBYCordinate, int expectedNumberOfLightsOn)
        {
            var christmasLightsGrid = new ChristmasLightsGrid();

            christmasLightsGrid.TurnOn(cornerAXCordinate, cornerAYCordinate, cornerBXCordinate, cornerBYCordinate);

            christmasLightsGrid.NumberOfLightsOn.Should().Be(expectedNumberOfLightsOn);
        }

        [Fact]
        public void TurnOnShouldTurnOnLightsOnProvidedCoordinates_WhenThereIsAlreadyTurnedOnLightsAndAreNotIntersected()
        {
            var christmasLightsGrid = new ChristmasLightsGrid();
            christmasLightsGrid.TurnOn(0, 0, 2, 2);

            christmasLightsGrid.TurnOn(4, 4, 6, 6);

            christmasLightsGrid.NumberOfLightsOn.Should().Be(18);
        }

        [Fact]
        public void TurnOnShouldTurnOnLightsOnProvidedCoordinates_WhenThereIsAlreadyTurnedOnLightsAndAreIntersected()
        {
            var christmasLightsGrid = new ChristmasLightsGrid();
            christmasLightsGrid.TurnOn(7, 14, 3, 12);

            christmasLightsGrid.TurnOn(4, 10, 8, 13);

            christmasLightsGrid.NumberOfLightsOn.Should().Be(27);
        }

        [Fact]
        public void TurnOffShouldTurnOffLightsOnProvidedCoordinates_WhenAllLightsAreOn()
        {
            var christmasLightsGrid = new ChristmasLightsGrid();
            christmasLightsGrid.TurnOn(0, 0, 999, 999);

            christmasLightsGrid.TurnOff(0, 0, 2, 2);

            christmasLightsGrid.NumberOfLightsOn.Should().Be(999991);
        }

        [Fact]
        public void TurnOffShouldTurnOffLightsOnProvidedCordinates_WhenThereIsAlreadyTurnedOffLightsAndAreNotIntersected()
        {
            var christmasLightsGrid = new ChristmasLightsGrid();
            christmasLightsGrid.TurnOn(0, 0, 999, 999);
            christmasLightsGrid.TurnOff(0, 0, 2, 2);

            christmasLightsGrid.TurnOff(4, 4, 6, 6);

            christmasLightsGrid.NumberOfLightsOn.Should().Be(999982);
        }

        [Fact]
        public void TurnOffShouldTurnOffLightsOnProvidedCordinates_WhenTherreIsAlreadyTurnedOffLightsAndAreIntersected()
        {
            var christmasLightsGrid = new ChristmasLightsGrid();
            christmasLightsGrid.TurnOn(0, 0, 999, 999);
            christmasLightsGrid.TurnOff(3, 3, 5, 5);

            christmasLightsGrid.TurnOff(4, 4, 6, 6);

            christmasLightsGrid.NumberOfLightsOn.Should().Be(999986);
        }

        [Fact]
        public void ToggleShouldTurnOffAllOnLights_And_TurnOnAllOffLights()
        {
            var christmasLightsGrid = new ChristmasLightsGrid();
            christmasLightsGrid.TurnOn(1, 1, 16, 16);
            christmasLightsGrid.TurnOff(4, 8, 6, 10);

            christmasLightsGrid.Toggle();

            christmasLightsGrid.NumberOfLightsOn.Should().Be(999753);
        }

        [Fact]
        public void TurnOnShouldTurnOnLights_AfterToggle()
        {
            var christmasLightsGrid = new ChristmasLightsGrid();
            christmasLightsGrid.TurnOn(0, 0, 998, 998);
            christmasLightsGrid.Toggle();

            christmasLightsGrid.TurnOn(1, 1, 16, 16);

            christmasLightsGrid.NumberOfLightsOn.Should().Be(2255);
        }
    }
}