using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Model.Matrix
{
    internal class MatrixG4x7
    {
        private int[,] _matrixG = new int[,]
        {
                { 1, 0, 0, 0, 1, 1, 1},
                { 0, 1, 0, 0, 1, 1, 1},
                { 0, 0, 1, 0, 1, 1, 0},
                { 0, 0, 0, 1, 1, 0, 1}
        };

        public int[,] MatrixG { get => _matrixG; }
    }
}
