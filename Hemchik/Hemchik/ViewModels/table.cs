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

        private string _cheackEightBit;
        public string CheackEightBit
        {
            get => _cheackEightBit;
            set
            {
                Set(ref _cheackEightBit, value);
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
            CheackEightBit = eightBit;
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
                List<int> eightBit = ConvertBinaryStringToList(_eightBit);

                int[] sindrom = GenerateSyndrom(syndrome);

                ObservableCollection<int> sindromCollection = new ObservableCollection<int>(sindrom);


                int countError = 0;
                List<int> indexError = new List<int>();

                for (int i = 0; i < matrixH.Count; i++)
                {
                    if (ObservableCollectionEquals(matrixH[i], sindromCollection))
                    {
                        complited = "ошибка в " + (i + 1).ToString() + " бите";
                        countError++;
                        indexError.Add(i);
                    }
                }


                string strCheackEightBit = "";
                if (countError == 0 || countError > 1)
                {
                    complited = "Больше 1 ошибки";

                    foreach (var item in eightBit)
                    {
                        strCheackEightBit += item.ToString();
                    }

                    CheackEightBit = strCheackEightBit;

                    return;
                }
                
                foreach (int i in indexError)
                {
                    if (eightBit[i] == 0)
                    {
                        eightBit[i] = 1;
                    }
                    else
                    {
                        eightBit[i] = 0;
                    }
                }

                foreach (var item in eightBit)
                {
                    strCheackEightBit += item.ToString();
                }

                CheackEightBit = strCheackEightBit;
            }
        }

        private List<int> ConvertBinaryStringToList(string binaryString)
        {
            List<int> binaryList = new List<int>();

            foreach (char bitChar in binaryString)
            {
                // Преобразуем символы '0' и '1' в целые числа 0 и 1
                int bit = bitChar == '0' ? 0 : 1;

                // Добавляем в список
                binaryList.Add(bit);
            }

            return binaryList;
        }

        private bool ObservableCollectionEquals<T>(ObservableCollection<T> collection1, ObservableCollection<T> collection2)
        {
            // Проверяем, что коллекции имеют одинаковую длину
            if (collection1.Count != collection2.Count)
            {
                return false;
            }

            // Проверяем, что элементы коллекций равны друг другу
            for (int i = 0; i < collection1.Count; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(collection1[i], collection2[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private int[] GenerateSyndrom(string strSyndrom)
        {
            int[] sindrom = new int[] { 0, 0, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                sindrom[i] = int.Parse(strSyndrom[i].ToString());
            }

            return sindrom;
        }
    }
}
