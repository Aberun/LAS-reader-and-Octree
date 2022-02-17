using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAS_reader
{
    class Octree
    {
        List<Point> finalPoints = new List<Point>();
        int rezolution = 100;
        static int totalPointCounter;

        Octree fnw;
        Octree fne;
        Octree fsw;
        Octree fse;
        Octree bnw;
        Octree bne;
        Octree bsw;
        Octree bse;
        public Octree(Cube boundry, List<Point> points)
        {
            if (rezolution <= points.Count)
            {
                double DivX = (boundry.GetX() - (boundry.GetLenght() / 2));
                double DivY = (boundry.GetY() - (boundry.GetWidth() / 2));
                double DivZ = (boundry.GetZ() - (boundry.GetDepth() / 2));
                double DivLenght = boundry.GetLenght() / 2;
                double DivWidth = boundry.GetWidth() / 2;
                double DivDepth = boundry.GetDepth() / 2;
                Cube fnwCube = new Cube(DivX, (DivY + DivWidth), DivZ, DivLenght, DivWidth, DivDepth);
                this.fnw = new Octree(fnwCube, GetPointsforCube(points, fnwCube));
                Cube fneCube = new Cube((DivX + DivLenght), (DivY + DivWidth), DivZ, DivLenght, DivWidth, DivDepth);
                this.fne = new Octree(fneCube, GetPointsforCube(points, fneCube));
                Cube fswCube = new Cube(DivX, DivY, DivZ, DivLenght, DivWidth, DivDepth);
                this.fsw = new Octree(fswCube, GetPointsforCube(points, fswCube));
                Cube fseCube = new Cube((DivX + DivLenght), DivY, DivZ, DivLenght, DivWidth, DivDepth);
                this.fse = new Octree(fseCube, GetPointsforCube(points, fseCube));
                Cube bnwCube = new Cube(DivX, (DivY + DivWidth), (DivZ + DivDepth), DivLenght, DivWidth, DivDepth);
                this.bnw = new Octree(bnwCube, GetPointsforCube(points, bnwCube));
                Cube bneCube = new Cube((DivX + DivLenght), (DivY + DivWidth), (DivZ + DivDepth), DivLenght, DivWidth, DivDepth);
                this.bne = new Octree(bneCube, GetPointsforCube(points, bneCube));
                Cube bswCube = new Cube(DivX, DivY, (DivZ + DivDepth), DivLenght, DivWidth, DivDepth);
                this.bsw = new Octree(bswCube, GetPointsforCube(points, bswCube));
                Cube bseCube = new Cube((DivX + DivLenght), DivY, (DivZ + DivDepth), DivLenght, DivWidth, DivDepth);
                this.bse = new Octree(bseCube, GetPointsforCube(points, bseCube));
            }
            else
            {
                finalPoints.AddRange(points);
                totalPointCounter += finalPoints.Count;
            }
        }
        public static void GetTotalCount()
        {
                Console.WriteLine(totalPointCounter);
        }
        private List<Point> GetPointsforCube(List<Point> points, Cube cube) 
        {
            List<Point> returnPoints = new List<Point>();
            foreach (Point singlepoint in points)
            {
                if ((singlepoint.GetX() <= cube.GetX() && singlepoint.GetX() >= cube.GetX() - cube.GetLenght() &&
                     singlepoint.GetY() <= cube.GetY() && singlepoint.GetY() >= cube.GetY() - cube.GetWidth() &&
                     singlepoint.GetZ() <= cube.GetZ() && singlepoint.GetZ() >= cube.GetZ() - cube.GetDepth()))
                {
                    returnPoints.Add(singlepoint);
                }
            }
            return returnPoints;
        }
    }
}
