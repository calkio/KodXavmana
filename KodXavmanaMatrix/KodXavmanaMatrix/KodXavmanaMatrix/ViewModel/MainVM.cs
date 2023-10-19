using KodXavmanaMatrix.Model;
using KodXavmanaMatrix.Model.Entity;
using KodXavmanaMatrix.Model.Matrix;
using KodXavmanaMatrix.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodXavmanaMatrix.ViewModel
{
    internal class MainVM : BaseVM
    {
        private int[] byteText;

        private int[] _firstArray;
        private int[] _secondArray;
        private int[] _thirdArray;
        private int[] _fourthArray;

        private int[,] matrixG;
        private int[,] matrixH;

        private string _strText;
        public string StrText
        {
            get
            {
                return _strText;
            }
            set
            {
                _strText = value;

                ConvertTextToByte convertTextToByte = new ConvertTextToByte();
                byteText = convertTextToByte.GetConvertTextToByte(_strText);

                MultiplyMatrix multiplyMatrix = new MultiplyMatrix();

                ParsingArray parsingArray = new ParsingArray(byteText);

                _firstArray = multiplyMatrix.GetMultiplicationMatrix(parsingArray.FirstArray, matrixG);
                _secondArray = multiplyMatrix.GetMultiplicationMatrix(parsingArray.SecondArray, matrixG);
                _thirdArray = multiplyMatrix.GetMultiplicationMatrix(parsingArray.ThirdArray, matrixG);
                _fourthArray = multiplyMatrix.GetMultiplicationMatrix(parsingArray.FourthArray, matrixG);

                UpdateObservableCollection();
                HuffmanBoundary = GetHuffmanBoundary();
                int a = 0;
            }
        }

        private ObservableCollection<FirstTable> _valueInFurstTable = new ObservableCollection<FirstTable>();
        public ObservableCollection<FirstTable> ValueInFurstTable { get => _valueInFurstTable; set => Set(ref _valueInFurstTable, value); }

        private string _huffmanBoundary;
        public string HuffmanBoundary { get => _huffmanBoundary; set => Set(ref _huffmanBoundary, value); }

        public MainVM()
        {
            MatrixG7x4 matrixG7X4 = new();
            MatrixH3x7 matrixH3x7 = new();

            matrixG = matrixG7X4.MatrixG;
            matrixH = matrixH3x7.MatrixH;
        }

        private void UpdateObservableCollection()
        {
            ValueInFurstTable.Clear();
            foreach (var item in new[]
            {
                GetNewFirstTable(_firstArray),
                GetNewFirstTable(_secondArray),
                GetNewFirstTable(_thirdArray),
                GetNewFirstTable(_fourthArray)
            })
            {
                ValueInFurstTable.Add(item);
            }
        }

        private FirstTable GetNewFirstTable(int[] dataArray)
        {
            FirstTable newFirstTable = new FirstTable();

            string informativeBits = dataArray[2].ToString() + dataArray[4].ToString() +
                                     dataArray[5].ToString() + dataArray[6].ToString();

            string parityBits = dataArray[0].ToString() + dataArray[1].ToString() +
                                dataArray[3].ToString();

            newFirstTable.FirstArray = string.Join("", informativeBits);
            newFirstTable.SecondArray = string.Join("", parityBits);
            newFirstTable.ThirdValue = GetBitChetnosti(dataArray[0]);

            return newFirstTable;
        }

        private int GetBitChetnosti(int firstValueParityBits)
        {
            if (firstValueParityBits == 0) return 0;
            else return 1;
        }

        private string GetHuffmanBoundary()
        {
            var colection = _valueInFurstTable.Where(x => x.ThirdValue == 1).Select(x => x);
            return colection.Count().ToString();
        }
    }
}
