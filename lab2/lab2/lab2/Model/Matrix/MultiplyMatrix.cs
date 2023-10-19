using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Model.Matrix
{
    internal class MultiplyMatrix
    {
        public int[] GetMultiplicationMatrix(int[] matrix1, int[,] matrix2)
        {
            int[] result = new int[matrix2.GetLength(1)];

            for (int i = 0; i < matrix2.GetLength(1); i++)
            {
                int res = 0;
                for (int j = 0; j < matrix1.Length; j++)
                {
                    res += matrix1[j] & matrix2[j, i];
                }
                if (res % 2 == 0) result[i] = 0;
                if (res % 2 == 1) result[i] = 1;
            }

            return result;
        }

        public int[] GetGetMultiplicationMatrixTransp(int[] matrix1, int[,] matrix2)
        {
            int[] result = new int[matrix2.GetLength(1)];

            for (int i = 0; i < matrix1.Length; i++)
            {
                int res = 0;
                for (int j = 0; j < matrix1.GetLength(0); j++)
                {
                    res += matrix1[i] & matrix2[j, i];
                }
                if (res % 2 == 0) result[i] = 0;
                if (res % 2 == 1) result[i] = 1;
            }

            return result;
        }
    }
}
