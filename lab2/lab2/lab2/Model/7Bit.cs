using lab2.Model.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Model
{
    internal class _7Bit
    {
        public string InformativeBits;
        public string ParityBits;
        public string ParityBit;
        public ParsingArray ParsingArray;
        public Dictionary<int, int[]> NumberValue = new Dictionary<int, int[]>
        {
            {0, new int[7]},
            {1, new int[7]},
            {2, new int[7]},
            {3, new int[7]}
        };



        public _7Bit(int[] binaryRepresentation) 
        {
            ParsingArray = new ParsingArray(binaryRepresentation);
            SetValueDict();
        }


        private void SetValueDict()
        {
            NumberValue[0] = Get7Bit(ParsingArray.FirstArray4Bit);
            NumberValue[1] = Get7Bit(ParsingArray.FirstArray4Bit);
            NumberValue[2] = Get7Bit(ParsingArray.FirstArray4Bit);
            NumberValue[3] = Get7Bit(ParsingArray.FirstArray4Bit);
        }

        private int[] Get7Bit(int[] bit4)
        {
            MultiplyMatrix multiplyMatrix = new MultiplyMatrix();
            MatrixG4x7 matrixG4X7 = new MatrixG4x7();

            return multiplyMatrix.GetMultiplicationMatrix(bit4, matrixG4X7.MatrixG);
        }
    }
}
