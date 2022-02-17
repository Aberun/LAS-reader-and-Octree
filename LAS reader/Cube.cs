namespace LAS_reader
{
    class Cube
    {
        double x, y, z, lenght, width, depth;

        public Cube(double x, double y, double z, double lenght, double width, double depth)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.lenght = lenght;
            this.width = width;
            this.depth = depth;
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
        public double GetLenght()
        {
            return lenght;
        }
        public double GetWidth()
        {
            return width;
        }
        public double GetDepth()
        {
            return depth;
        }

    }

}
