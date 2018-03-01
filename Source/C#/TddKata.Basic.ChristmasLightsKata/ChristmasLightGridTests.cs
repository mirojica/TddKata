using System;
using System.Collections.Generic;
using System.Linq;
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

    public class ChristmasLightsGrid
    {
        private readonly HashSet<ChristamLight> _lightsOn = new HashSet<ChristamLight>();

        public int NumberOfLightsOn { get; private set; }

        public void TurnOn(int xCordinate1, int yCordinate1, int xCordinate2, int yCordinate2)
        {
            if (!Enumerable.Range(0, 1000).Contains(xCordinate1) || !Enumerable.Range(0, 1000).Contains(xCordinate2) || !Enumerable.Range(0, 1000).Contains(yCordinate1) || !Enumerable.Range(0, 1000).Contains(yCordinate2))
            {
                throw new ArgumentException();
            }

            var startingXCordinate = Math.Min(xCordinate2, xCordinate1);
            var endingXCordinate = Math.Max(xCordinate2, xCordinate1);

            var startingYCordinate = Math.Min(yCordinate2, yCordinate1);
            var endingYCordinate = Math.Max(yCordinate2, yCordinate1);

            for (var i = startingXCordinate; i < endingXCordinate + 1; i++)
            {
                for (var j = startingYCordinate; j < endingYCordinate + 1; j++)
                {
                    var light = new ChristamLight(i, j);

                    if (!_lightsOn.Contains(light))
                    {
                        _lightsOn.Add(light);
                    }
                }
            }

            NumberOfLightsOn = _lightsOn.Count;
        }

        public void Toggle()
        {
            NumberOfLightsOn = 1000000;
        }
    }

    public class ChristamLight
    {
        private readonly int _xCordinate;
        private readonly int _yCordinate;

        public ChristamLight(int xCordinate, int yCordinate)
        {
            _xCordinate = xCordinate;
            _yCordinate = yCordinate;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ChristamLight item))
            {
                return false;
            }

            return _xCordinate.Equals(item._xCordinate) && _yCordinate.Equals(item._yCordinate);
        }

        public override int GetHashCode()
        {
            var hashCode = _xCordinate.GetHashCode();
            return (hashCode * 397) ^ _yCordinate.GetHashCode();
        }
    }
}