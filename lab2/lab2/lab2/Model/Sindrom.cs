using lab2.Model.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Model
{
    internal class Sindrom
    {
        public int[] GetSindrom(int[] myArray)
        {
            MultiplyMatrix multiplyMatrix = new MultiplyMatrix();
            MatrixH3x7 matrixH3X7 = new MatrixH3x7();

            return multiplyMatrix.GetMultiplicationMatrixTransp(myArray, matrixH3X7.MatrixH);
        }
    }
}
