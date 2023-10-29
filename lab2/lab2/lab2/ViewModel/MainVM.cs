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

        private ObservableCollection<SecondTable> _secondTable = new ObservableCollection<SecondTable>();
        public ObservableCollection<SecondTable> SecondTables { get => _secondTable; set => Set(ref _secondTable, value); }

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

                _7Bit _7Bit = new _7Bit(binaryRepresentation);

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
            FirstTables.Add(CreateFirstTable(multiplyMatrix.GetMultiplicationMatrix(parsingArray.FirstArray4Bit, matrixG4X7.MatrixG)));
            FirstTables.Add(CreateFirstTable(multiplyMatrix.GetMultiplicationMatrix(parsingArray.SecondArray4Bit, matrixG4X7.MatrixG)));
            FirstTables.Add(CreateFirstTable(multiplyMatrix.GetMultiplicationMatrix(parsingArray.ThirdArray4Bit, matrixG4X7.MatrixG)));
            FirstTables.Add(CreateFirstTable(multiplyMatrix.GetMultiplicationMatrix(parsingArray.FourthArray4Bit, matrixG4X7.MatrixG)));
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
            GenerateSecondTables();
            SetSindrom();
            SetParams();
        }

        private void GenerateSecondTables()
        {
            for (int i = 0; i < 4; i++)
            {
                SecondTables.Add(new SecondTable());
            }
        }

        private void SetSindrom()
        {
            ParsingArray parsingArray = new ParsingArray(binaryRepresentation);

            List<int[]> all7Bit = new List<int[]>();
            all7Bit.Add(Get7Bit(parsingArray.FirstArray4Bit));
            all7Bit.Add(Get7Bit(parsingArray.SecondArray4Bit));
            all7Bit.Add(Get7Bit(parsingArray.ThirdArray4Bit));
            all7Bit.Add(Get7Bit(parsingArray.FourthArray4Bit));

            List<int[]> allSindrom = new List<int[]>();
            for (int i = 0; i < SecondTables.Count; i++)
            {
                var selectedSindrom = MultiplyTransp(all7Bit[i]);

                SecondTables[i].Sindrom = string.Join("", selectedSindrom); 
            }
        }

        private int[] Get7Bit(int[] myArray)
        {
            MultiplyMatrix multiplyMatrix = new MultiplyMatrix();
            MatrixG4x7 matrixG4X7 = new MatrixG4x7();

            return multiplyMatrix.GetMultiplicationMatrix(myArray, matrixG4X7.MatrixG);
        }

        private int[] MultiplyTransp(int[] myArray)
        {
            MultiplyMatrix multiplyMatrix = new MultiplyMatrix();
            MatrixH3x7 matrixH3X7 = new MatrixH3x7();

            return multiplyMatrix.GetMultiplicationMatrixTransp(myArray, matrixH3X7.MatrixH);
        }

        private void SetParams()
        {
            ParsingArray parsingArray = new ParsingArray(binaryRepresentation);

            List<int[]> all7Bit = new List<int[]>();
            all7Bit.Add(Get7Bit(parsingArray.FirstArray4Bit));
            all7Bit.Add(Get7Bit(parsingArray.SecondArray4Bit));
            all7Bit.Add(Get7Bit(parsingArray.ThirdArray4Bit));
            all7Bit.Add(Get7Bit(parsingArray.FourthArray4Bit));

            for (int i = 0; i < all7Bit.Count; i++)
            {
                int[] informativeBits = new int[4]; // Первые 4 бита как информативные биты
                int[] parityBits = new int[3]; // Последние 3 бита как проверочные биты

                Array.Copy(all7Bit[i], 0, informativeBits, 0, 4);
                Array.Copy(all7Bit[i], 4, parityBits, 0, 3);

                SecondTables[i].InformativeBits = string.Join("", informativeBits);
                SecondTables[i].ParityBits = string.Join("", parityBits);
                SecondTables[i].ParityBit = GetParityBit(all7Bit[i]); 
            }
        }

        #endregion
    }
}
