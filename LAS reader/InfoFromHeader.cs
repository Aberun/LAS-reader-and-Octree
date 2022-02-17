using System;

namespace LAS_reader
{
    class InfoFromHeader
    {
        int info;

        public InfoFromHeader(int[] byteLenght, byte[] readFile)
        {
            byte[] convertToInt = new byte[byteLenght[1] - byteLenght[0]];
            if (convertToInt.Length == 4)
            {
                for (int i = byteLenght[0]; i < byteLenght[1]; i++)
                {
                    convertToInt[i - byteLenght[0]] = readFile[i];
                }
                info = BitConverter.ToInt32(convertToInt, 0);
            }
            else
            {
                for (int i = byteLenght[0]; i < byteLenght[1]; i++)
                {
                    convertToInt[i - byteLenght[0]] = readFile[i];
                }
                info = BitConverter.ToInt16(convertToInt, 0);
            }
        }
        public int GetHeaderInfo()
        {
            return info;
        }
    }
}
