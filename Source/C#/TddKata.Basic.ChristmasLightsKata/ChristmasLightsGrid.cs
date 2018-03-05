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
            var coordinateMap = ExctractCoordinates(0, 0, 999, 999);

            foreach (var rowCoordinates in coordinateMap)
            {
                var christmasLights = rowCoordinates.Value.Select(coordinate => new ChristmasLight(coordinate)).ToList();
                _christmasLightsMap.Add(rowCoordinates.Key, christmasLights);
            }
        }

        public void TurnOn(int xCordinate1, int yCordinate1, int xCordinate2, int yCordinate2)
        {
            if (CordinatesAreNotInRange(xCordinate1, yCordinate1, xCordinate2, yCordinate2))
            {
                throw new ArgumentException();
            }

            var coordinateMap = ExctractCoordinates(xCordinate1, yCordinate1, xCordinate2, yCordinate2);

            foreach (var rowCoordinates in coordinateMap)
            {
                var lights = _christmasLightsMap[rowCoordinates.Key];
                lights.Where(light => rowCoordinates.Value.Contains(light.Coordinate)).ToList().ForEach(light => light.TurnOn());
            }
        }

        public void TurnOff(int xCordinate1, int yCordinate1, int xCordinate2, int yCordinate2)
        {
            if (CordinatesAreNotInRange(xCordinate1, yCordinate1, xCordinate2, yCordinate2))
            {
                throw new ArgumentException();
            }

            var coordinateMap = ExctractCoordinates(xCordinate1, yCordinate1, xCordinate2, yCordinate2);

            foreach (var rowCoordinates in coordinateMap)
            {
                var lights = _christmasLightsMap[rowCoordinates.Key];
                lights.Where(light => rowCoordinates.Value.Contains(light.Coordinate)).ToList().ForEach(light => light.TurnOff());
            }
        }

        public void Toggle()
        {
            foreach (var christamLightRow in _christmasLightsMap)
            {
                foreach (var christamLight in christamLightRow.Value)
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

        private static bool CordinatesAreNotInRange(int xCordinate1, int yCordinate1, int xCordinate2, int yCordinate2)
        {
            return !(Enumerable.Range(0, 1000).Contains(xCordinate1) && 
                     Enumerable.Range(0, 1000).Contains(xCordinate2) && 
                     Enumerable.Range(0, 1000).Contains(yCordinate1) && 
                     Enumerable.Range(0, 1000).Contains(yCordinate2));
        }

        private IDictionary<int, IList<Coordinate>> ExctractCoordinates(int xCordinate1, int yCordinate1, int xCordinate2, int yCordinate2)
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