using lab2.Model;
using lab2.Model.Matrix;
using lab2.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.ViewModel
{
    internal class MainVM: BaseVM
    {
        private int[] binaryRepresentation;

        private ObservableCollection<FirstTable> _firstTables = new ObservableCollection<FirstTable>();
        public ObservableCollection<FirstTable> FirstTables { get => _firstTables; set => Set(ref _firstTables, value); }

        private ObservableCollection<FirstTable> _secondTable = new ObservableCollection<FirstTable>();
        public ObservableCollection<FirstTable> SecondTables { get => _secondTable; set => Set(ref _secondTable, value); }

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

                ConvertStringToByte convertStringToByte = new ConvertStringToByte();
                binaryRepresentation = convertStringToByte.GetConvertTextToByte(_strText);

                AddValueInFirstTabel();
                AddValueInSecondTable();
            }
        }

        #region Для первой таблицы

        private void AddValueInFirstTabel()
        {
            ParsingArray parsingArray = new ParsingArray(binaryRepresentation);

            MultiplyMatrix multiplyMatrix = new MultiplyMatrix();
            MatrixG4x7 matrixG4X7 = new MatrixG4x7();


            FirstTables.Clear();
            FirstTables.Add(CreateFirstTable(multiplyMatrix.GetMultiplicationMatrix(parsingArray.FirstArray, matrixG4X7.MatrixG)));
            FirstTables.Add(CreateFirstTable(multiplyMatrix.GetMultiplicationMatrix(parsingArray.SecondArray, matrixG4X7.MatrixG)));
            FirstTables.Add(CreateFirstTable(multiplyMatrix.GetMultiplicationMatrix(parsingArray.ThirdArray, matrixG4X7.MatrixG)));
            FirstTables.Add(CreateFirstTable(multiplyMatrix.GetMultiplicationMatrix(parsingArray.FourthArray, matrixG4X7.MatrixG)));
        }

        private FirstTable CreateFirstTable(int[] myArray)
        {
            int[] informativeBits = new int[4]; // Первые 4 бита как информативные биты
            int[] parityBits = new int[3]; // Последние 3 бита как проверочные биты

            // Копирование первых 4 элементов в informativeBits
            Array.Copy(myArray, 0, informativeBits, 0, 4);
            // Копирование оставшихся 3 элементов в parityBits
            Array.Copy(myArray, 4, parityBits, 0, 3);

            FirstTable firstTable = new FirstTable();
            firstTable.InformativeBits = string.Join("", informativeBits);
            firstTable.ParityBits = string.Join("", parityBits);
            firstTable.ParityBit = GetParityBit(myArray);

            return firstTable;
        }

        private int GetParityBit(int[] myArray)
        {
            int parityBit = 0;
            foreach (char bit in myArray)
            {
                if (bit == 1)
                {
                    parityBit ^= 1; // Применяем операцию XOR для рассчета бита четности
                }
            }

            return parityBit;
        }

        #endregion

        #region Для второй таблицы

        private void AddValueInSecondTable()
        {

        }

        #endregion
    }
}
