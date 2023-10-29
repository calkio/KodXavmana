using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Model
{
    internal class ParsingArray
    {
        public int[] FirstArray4Bit;
        public int[] SecondArray4Bit;
        public int[] ThirdArray4Bit;
        public int[] FourthArray4Bit;


        public ParsingArray(int[] inputArray)
        {
            var subArray = GetParsingArray(inputArray);
            InitArray(subArray);
        }

        private int[][] GetParsingArray(int[] inputArray)
        {
            int[][] subArray = new int[4][];
            for (int i = 0; i < 4; i++)
            {
                subArray[i] = new int[4];
                Array.Copy(inputArray, i * 4, subArray[i], 0, 4);
            }
            return subArray;
        }

        private void InitArray(int[][] subArray)
        {
            FirstArray4Bit = subArray[0];
            SecondArray4Bit = subArray[1];
            ThirdArray4Bit = subArray[2];
            FourthArray4Bit = subArray[3];
        }
    }
}
