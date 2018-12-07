using System;

namespace Day6
{
    public class Location
    {
        public int Nr { get; set; }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }

        public int SumOfTheDistances { get; set; }
        public int CalculateDistance(int x, int y)
        {
            return Math.Abs(x - CoordinateX) + Math.Abs(y - CoordinateY);
        }
    }
}