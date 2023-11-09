using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Model
{
    internal class ErrorCode
    {
        private List<int> single = new List<int>()
        {
            1,0,0,0,   0,0,0/*,0,   0,0,0,0,   0,0,0,0,   0,0,0,0,   0,0,0,0,   0,0,0,0,   0,0,0,0,   0,0,0,0,   0,0,0,0,   0,0,0,0,   0,0,0,0,   0,0,0,0,   0,0,0,0,   0,0,0,0,   0,0,0*/
        };

        private List<int> rx = new List<int>();



        public int GetIndexError(string errorText, List<int> gX)
        {
            List<int> rX = GetrX(gX); // То, что нужно получить

            List<int> errorTextList = ConvertStringList(errorText); // Переводим из строки в лист чисел
            List<int> check = new();
            List<int> ErrortTextCopy = CreateCopy(errorTextList);
            bool isCoincided = false;
            int index = 0;
            do
            {
                check = CycleXor(errorTextList, gX);

                if (check == rX) isCoincided = true;
                else
                {
                    index++;
                    errorTextList = ErrortTextCopy;
                }

            } while (!isCoincided); 

            return index;
        }



        private List<int> ConvertStringList(string inputText)
        {
            List<int> result = new List<int>();
            foreach (var item in inputText)
            {
                result.Add((int)Char.GetNumericValue(item));
            }

            return result;
        }


        private List<int> GetrX(List<int> gX)
        {
            return CycleXor(Xor(single, gX), gX);
        }


        private List<int> CreateCopy(List<int> errorText)
        {
            List<int> result = new List<int>();
            foreach (int i in errorText)
            {
                result.Add(i);
            }
            return result;
        }


        private List<int> CycleXor(List<int> errorText, List<int> gX)
        {
            List<int> result = new();
            do
            {
                result = Xor(errorText, gX);
            } while (result.Count >= gX.Count); // Ксорим пока значение не станет короче полинома

            return result;
        }


        private List<int> Xor(List<int> firstValue, List<int> secondValue)
        {
            for (int i = 0; i < secondValue.Count; i++)
            {
                switch (firstValue[i] + secondValue[i]) // Xor
                {
                    case 0:
                        firstValue[i] = 0;
                        break;
                    case 1:
                        firstValue[i] = 1;
                        break;
                    case 2:
                        firstValue[i] = 0;
                        break;
                }
            }
            for (int i = 0, j = 0; i < firstValue.Count; i++) // Удаление первого бита, потому что мы понижаем степень
            {
                if (firstValue[i - j] == 0)
                {
                    firstValue.RemoveAt(i - j);
                    j++;
                }
                else break;
            }

            return firstValue;
        }
    }
}
