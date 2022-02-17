using System;
using System.Collections.Generic;
using System.IO;

namespace LAS_reader
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = "C:\\LAS\\lake.las";
            string path = "C:\\LAS\\2743_1234.las";
            int offsetToPointData, numberOfPointsRecords, sizeOfPointRecord;
            int[] offsetToPointDataArray = new int[2] { 96, 100 };
            int[] numberOfPointsRecordsArray = new int[2] { 107, 111 };
            int[] sizeOfPointRecordArray = new int[2] { 105, 107 };
            byte[] fileByteArray = File.ReadAllBytes(path);

            InfoFromHeader headerInfoOffset = new InfoFromHeader(offsetToPointDataArray, fileByteArray);
            offsetToPointData = headerInfoOffset.GetHeaderInfo();
            InfoFromHeader headerSizeOfPoint = new InfoFromHeader(sizeOfPointRecordArray, fileByteArray);
            sizeOfPointRecord = headerSizeOfPoint.GetHeaderInfo();
            InfoFromHeader headerNumberOfPoints = new InfoFromHeader(numberOfPointsRecordsArray, fileByteArray);
            numberOfPointsRecords = headerNumberOfPoints.GetHeaderInfo();
            Console.WriteLine(numberOfPointsRecords);

            int counterSettings = 0;
            byte[] readSettingsArray = new byte[8];
            double[] settingsArray = new double[12];
            for (int i = 0; i <= 88; i += 8)
            {
                for (int j = 131 + i; j < 139 + i; j++)
                {
                    readSettingsArray[j - (131 + i)] = fileByteArray[j];
                }
                settingsArray[counterSettings] = BitConverter.ToDouble(readSettingsArray, 0);
                counterSettings++;
            }
            // settingsArray X scale factor [0], Y scale factor [1], Z scale factor [2], X offset [3], Y offset [4], Z offset [5],
            // Max X [6], Min X [7], Max Y [8], Min Y [9], Max Z [10], Min Z [11]

            List<Point> points = new List<Point>();
            int classCounter = 0;
            for (int i = offsetToPointData; i < (numberOfPointsRecords * sizeOfPointRecord) + offsetToPointData; i += sizeOfPointRecord)
            {
                int x = GetPointCoord(i, 0, fileByteArray);
                int y = GetPointCoord(i, 4, fileByteArray);
                int z = GetPointCoord(i, 8, fileByteArray);

                points.Add(new Point(settingsArray[0], settingsArray[1], settingsArray[2],
                                     settingsArray[3], settingsArray[4], settingsArray[5], x, y, z));
                classCounter++;
            }

            Cube root = new Cube(settingsArray[6], settingsArray[8], settingsArray[10], (settingsArray[6] - settingsArray[7]), (settingsArray[8] - settingsArray[9]), (settingsArray[10] - settingsArray[11]));
            Octree oktree = new Octree(root, points);
            Octree.GetTotalCount();

            Console.ReadKey();
        }
        public static int GetPointCoord(int allPointsNumber, int pointCoordinteIndex, byte[] fileByteArray)
        {
            byte[] pointCoord = new byte[4];
            for (int x = allPointsNumber + pointCoordinteIndex; x < allPointsNumber + pointCoordinteIndex + 4; x++)
            {
                int counter = allPointsNumber;
                pointCoord[x - (counter + pointCoordinteIndex)] = fileByteArray[x];
            }
            return BitConverter.ToInt32(pointCoord, 0);
        }
    }
}
