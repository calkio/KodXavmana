using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Model.Matrix
{
    internal class MatrixH3x7
    {
        private int[,] _matrixH = new int[,]
        {
                { 1, 1, 0, 1, 1, 0, 0},
                { 1, 1, 1, 0, 0, 1, 0},
                { 1, 1, 1, 1, 0, 0, 1}
        };

        public int[,] MatrixH { get => _matrixH; }
    }
}
