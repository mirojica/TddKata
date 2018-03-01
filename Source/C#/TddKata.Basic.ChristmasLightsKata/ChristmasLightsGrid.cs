using System;
using System.Collections.Generic;
using System.Linq;

namespace TddKata.Basic.ChristmasLightsKata
{
    public class ChristmasLightsGrid
    {
        private readonly HashSet<ChristamLight> _lightsOn = new HashSet<ChristamLight>();

        public int NumberOfLightsOn { get; private set; }

        public void TurnOn(int xCordinate1, int yCordinate1, int xCordinate2, int yCordinate2)
        {
            if (CordinatesAreNotInRange(xCordinate1, yCordinate1, xCordinate2, yCordinate2))
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

        private static bool CordinatesAreNotInRange(int xCordinate1, int yCordinate1, int xCordinate2, int yCordinate2)
        {
            return !(Enumerable.Range(0, 1000).Contains(xCordinate1) && 
                    Enumerable.Range(0, 1000).Contains(xCordinate2) && 
                    Enumerable.Range(0, 1000).Contains(yCordinate1) && 
                    Enumerable.Range(0, 1000).Contains(yCordinate2));
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