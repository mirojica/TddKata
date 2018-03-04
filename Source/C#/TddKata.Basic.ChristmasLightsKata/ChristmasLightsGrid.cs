using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TddKata.Basic.ChristmasLightsKata
{
    public class ChristmasLightsGrid
    {
        private readonly List<ChristamLight> _lights = new List<ChristamLight>(1000000);

        public int NumberOfLightsOn => _lights.Count(light => light.On);

        public ChristmasLightsGrid()
        {
            var coordinates = ExctractCoordinates(0, 0, 999, 999);

            foreach (var coordinate in coordinates)
            {
                _lights.Add(new ChristamLight(coordinate));
            }
        }

        public void TurnOn(int xCordinate1, int yCordinate1, int xCordinate2, int yCordinate2)
        {
            if (CordinatesAreNotInRange(xCordinate1, yCordinate1, xCordinate2, yCordinate2))
            {
                throw new ArgumentException();
            }

            var coordinates = ExctractCoordinates(xCordinate1, yCordinate1, xCordinate2, yCordinate2);

            //foreach (var coordinate in coordinates)
            //{
            //    _lights.First(light => light.Coordinate.Equals(coordinate)).TurnOn();
            //}

            Parallel.ForEach(coordinates,
                coordinate => _lights.First(light => light.Coordinate.Equals(coordinate)).TurnOn());
        }

        public void TurnOff(int xCordinate1, int yCordinate1, int xCordinate2, int yCordinate2)
        {
            if (CordinatesAreNotInRange(xCordinate1, yCordinate1, xCordinate2, yCordinate2))
            {
                throw new ArgumentException();
            }

            var coordinates = ExctractCoordinates(xCordinate1, yCordinate1, xCordinate2, yCordinate2);

            foreach (var coordinate in coordinates)
            {
                _lights.First(light => light.Coordinate.Equals(coordinate)).TurnOff();
            }
        }

        public void Toggle()
        {
            foreach (var christamLight in _lights)
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

        private static bool CordinatesAreNotInRange(int xCordinate1, int yCordinate1, int xCordinate2, int yCordinate2)
        {
            return !(Enumerable.Range(0, 1000).Contains(xCordinate1) && 
                     Enumerable.Range(0, 1000).Contains(xCordinate2) && 
                     Enumerable.Range(0, 1000).Contains(yCordinate1) && 
                     Enumerable.Range(0, 1000).Contains(yCordinate2));
        }

        private IEnumerable<Coordinate> ExctractCoordinates(int xCordinate1, int yCordinate1, int xCordinate2, int yCordinate2)
        {
            var coordinates = new List<Coordinate>();

            var startingXCordinate = Math.Min(xCordinate2, xCordinate1);
            var endingXCordinate = Math.Max(xCordinate2, xCordinate1) + 1;

            var startingYCordinate = Math.Min(yCordinate2, yCordinate1);
            var endingYCordinate = Math.Max(yCordinate2, yCordinate1) + 1;

            for (var i = startingXCordinate; i < endingXCordinate; i++)
            {
                for (var j = startingYCordinate; j < endingYCordinate; j++)
                {
                    coordinates.Add(new Coordinate(i, j));
                }
            }

            return coordinates;
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

    public class ChristamLight
    {
        public Coordinate Coordinate { get; }
        public bool On { get; private set; }

        public ChristamLight(Coordinate coordinate)
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
            if (!(obj is ChristamLight item))
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