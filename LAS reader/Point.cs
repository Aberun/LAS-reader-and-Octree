using System;

namespace LAS_reader
{
    class Point
    {
        double x, y, z;

        public Point(double xScale, double yScale, double zScale, double xOffset, double yOffset, double zOffset, int xCoord, int yCoord, int zCoord)
        {
            x = (xCoord * xScale) + xOffset;
            y = (yCoord * yScale) + yOffset;
            z = (zCoord * zScale) + zOffset;
        }
        public string GetInfo()
        {
            return "X: " + Convert.ToString(x) + " Y: " + Convert.ToString(y) + " Z: " + Convert.ToString(z);
        }
        public double GetX()
        {
            return x;
        }
        public double GetY()
        {
            return y;
        }
        public double GetZ()
        {
            return z;
        }
    }
}
