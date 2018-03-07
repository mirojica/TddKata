using System;
using System.Collections.Generic;
using System.Linq;

namespace TddKata.Basic.ChristmasLightsKata
{
    public class ChristmasLightsGrid
    {
        private readonly IDictionary<int, IList<ChristmasLight>> _christmasLightsMap;

        public int NumberOfLightsOn => _christmasLightsMap.Values.SelectMany(light => light).Count(light => light.On);

        public ChristmasLightsGrid()
        {
            _christmasLightsMap = new Dictionary<int, IList<ChristmasLight>>();
            var coordinateMap = ExctractCoordinatesOfAllRequestedLights(0, 0, 999, 999);

            foreach (var rowCoordinates in coordinateMap)
            {
                var christmasLights = rowCoordinates.Value.Select(coordinate => new ChristmasLight(coordinate)).ToList();
                _christmasLightsMap.Add(rowCoordinates.Key, christmasLights);
            }
        }

        public void TurnOn(int cornerAXCordinate, int cornerAYCordinate, int cornerBXCordinate, int cornerBYCordinate)
        {
            ValidateThatCordinatesAreInRange(cornerAXCordinate, cornerAYCordinate, cornerBXCordinate, cornerBYCordinate);

            var coordinateMap = ExctractCoordinatesOfAllRequestedLights(cornerAXCordinate, cornerAYCordinate, cornerBXCordinate, cornerBYCordinate);

            foreach (var rowCoordinates in coordinateMap)
            {
                var lights = _christmasLightsMap[rowCoordinates.Key];
                lights.Where(light => rowCoordinates.Value.Contains(light.Coordinate)).ToList().ForEach(light => light.TurnOn());
            }
        }

        public void TurnOff(int cornerAXCordinate, int cornerAYCordinate, int cornerBXCordinate, int cornerBYCordinate)
        {
            ValidateThatCordinatesAreInRange(cornerAXCordinate, cornerAYCordinate, cornerBXCordinate, cornerBYCordinate);

            var coordinateMap = ExctractCoordinatesOfAllRequestedLights(cornerAXCordinate, cornerAYCordinate, cornerBXCordinate, cornerBYCordinate);

            foreach (var rowCoordinates in coordinateMap)
            {
                var lights = _christmasLightsMap[rowCoordinates.Key];
                lights.Where(light => rowCoordinates.Value.Contains(light.Coordinate)).ToList().ForEach(light => light.TurnOff());
            }
        }

        public void Toggle()
        {
            foreach (var christamLightGridRow in _christmasLightsMap)
            {
                foreach (var christamLight in christamLightGridRow.Value)
                {
                    if (christamLight.On)
                    {
                        christamLight.TurnOff();
                    }
                    else
                    {
                        christamLight.TurnOn();
                    }
                }
            }
        }

        private static void ValidateThatCordinatesAreInRange(int cornerAXCordinate, int cornerAYCordinate, int cornerBXCordinate, int cornerBYCordinate)
        {
            if (!(Enumerable.Range(0, 1000).Contains(cornerAXCordinate) &&
                  Enumerable.Range(0, 1000).Contains(cornerBXCordinate) &&
                  Enumerable.Range(0, 1000).Contains(cornerAYCordinate) &&
                  Enumerable.Range(0, 1000).Contains(cornerBYCordinate)))
            {
                throw new ArgumentException();
            }
        }

        private static IDictionary<int, IList<Coordinate>> ExctractCoordinatesOfAllRequestedLights(int xCordinate1, int yCordinate1, int xCordinate2, int yCordinate2)
        {
            var coordinateMap = new Dictionary<int, IList<Coordinate>>();

            var startingXCordinate = Math.Min(xCordinate2, xCordinate1);
            var endingXCordinate = Math.Max(xCordinate2, xCordinate1) + 1;

            var startingYCordinate = Math.Min(yCordinate2, yCordinate1);
            var endingYCordinate = Math.Max(yCordinate2, yCordinate1) + 1;

            for (var i = startingXCordinate; i < endingXCordinate; i++)
            {
                coordinateMap.Add(i, new List<Coordinate>());
                for (var j = startingYCordinate; j < endingYCordinate; j++)
                {
                    coordinateMap[i].Add(new Coordinate(i, j));
                }
            }

            return coordinateMap;
        }
    }

    public class Coordinate
    {
        private readonly int _xCordinate;
        private readonly int _yCordinate;

        public Coordinate(int xCordinate, int yCordinate)
        {
            _xCordinate = xCordinate;
            _yCordinate = yCordinate;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Coordinate item))
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

    public class ChristmasLight
    {
        public Coordinate Coordinate { get; }
        public bool On { get; private set; }

        public ChristmasLight(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public void TurnOn()
        {
            On = true;
        }

        public void TurnOff()
        {
            On = false;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ChristmasLight item))
            {
                return false;
            }

            return Coordinate.Equals(item.Coordinate);
        }

        public override int GetHashCode()
        {
            return Coordinate.GetHashCode();
        }
    }
}