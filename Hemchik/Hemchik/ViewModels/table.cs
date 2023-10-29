using Hemchik.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hemchik.ViewModels
{
    internal class table : ViewModelBase
    {

        public ObservableCollection<ObservableCollection<int>> matrixH
        = new ObservableCollection<ObservableCollection<int>>()
             {
              new ObservableCollection<int> { 1, 0, 0, 0 },
              new ObservableCollection<int> { 1, 1, 1, 1 },
              new ObservableCollection<int> { 1, 1, 1, 1 },
              new ObservableCollection<int> { 1, 1, 1, 0 },
              new ObservableCollection<int> { 1, 1, 0, 1 },
              new ObservableCollection<int> { 1, 1, 0, 0 },
              new ObservableCollection<int> { 1, 0, 1, 0 },
              new ObservableCollection<int> { 1, 0, 0, 1 }
             };

        public ObservableCollection<ObservableCollection<int>> matrixG
       = new ObservableCollection<ObservableCollection<int>>()
            {
              new ObservableCollection<int> { 1, 0, 0, 0, 1, 1, 1 },
              new ObservableCollection<int> { 0, 1, 0, 0, 1, 1, 1 },
              new ObservableCollection<int> { 0, 0, 1, 0, 1, 1, 0 },
              new ObservableCollection<int> { 0, 0, 0, 1, 1, 0, 1 }
            };


        private int[] MatrixConverToVector(string fourbit)
        {
            int[] vector = new int[7]
                {0,0,0,0,0,0,0 };

          

            for (int i=0; i<4; i++)
            {
                if (fourbit[i] == '1')
                {
                    for (int j = 0; j < 7; j++)
                    {
                        vector[j] = vector[j] ^ matrixG[i][j];
                    }
                }
            }

            return vector;
        }

        private int[] SindromCalculate(string sevenbit)
        {
            int[] sindrom = new int[4]
                {0,0,0,0};



            for (int i = 0; i < 8; i++)
            {
                if (eightBit[i] == '1')
                {
                    for (int j = 0; j < 4; j++)
                    {
                        sindrom[j] = sindrom[j] ^ matrixH[i][j];
                    }
                }
            }

            return sindrom;
        }

        private int IsBitPartity(int[] vector)
        {
            int partity = 1;

            int summ = 0;
            foreach (var item in vector)
            {
                summ += item;
            }

            if (summ % 2 == 0) partity = 0;

            return partity;
        }

        private string VectorToString(int[] vector)
        {
            string final="";

            foreach (var item in vector)
            {
                final += item.ToString();
            }

            return final;
        }


        string _fourBit;
       
        public string fourBit
        {
            get => _fourBit;
            set { Set(ref _fourBit, value); }
        }

        private string _eightBit;
        public string eightBit
        {
            get => _eightBit;
            set { Set(ref _eightBit, value);
            }
        }
        private string _complited;
        public string complited
        {
            get => _complited;
            set
            {
                Set(ref _complited, value);
            }
        }

        private int _partityBit;
        public int partityBit
        {
            get => _partityBit;
            set { Set(ref _partityBit, value); }
        }

        private string _syndrome;
        public string syndrome
        {
            get => _syndrome;
            set { Set(ref _syndrome, value); }
        }

        public table(string bit4) 
        {
            fourBit = bit4;
            int[] vector = MatrixConverToVector(fourBit);
            eightBit = VectorToString(vector);
            partityBit = IsBitPartity(vector);
            eightBit = partityBit.ToString() + eightBit;
            syndrome = VectorToString(SindromCalculate(eightBit));
            IsComplited();
        }

        public void Refresh(string el)
        {
            Set(ref _eightBit, el);
            syndrome = VectorToString(SindromCalculate(eightBit));
            IsComplited();
        }

        private void IsComplited()
        {
            if (syndrome == "0000")
            { 
                complited = "ошибок нет"; 
            }
            else
            {
                int[] sindrom = new int[4]
                {0,0,0,0};

                for (int i = 0; i < 4; i++)
                { 
                    sindrom[i] = int.Parse(syndrome[i].ToString());
                }

                bool flag = false;

                for (int i = 0; i < 8; i++)
                {
                    flag = true;
                    for (int j = 0; j < 4; j++)
                    {
                        if (sindrom[j] != matrixH[i][j]) flag = false;
                    }
                    if (flag == true)
                    {
                        complited = "ошибка в " + (i + 1).ToString() + " бите";
                        return;
                    }
                }
            }
        }
    }
}
