using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Model
{
    internal class ParsingArray
    {
        public int[] FirstArray;
        public int[] SecondArray;
        public int[] ThirdArray;
        public int[] FourthArray;


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
            FirstArray = subArray[0];
            SecondArray = subArray[1];
            ThirdArray = subArray[2];
            FourthArray = subArray[3];
        }
    }
}
