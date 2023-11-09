using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Model
{
    internal class CodeCombination
    {
        public List<int> GetCodeCombination(string inputText, List<int> gx)
        {
            List<int> inputTextList = ConvertStringList(inputText); // Переводим из строки в лист чисел
            List<int> shiftLeft = ShiftLeft(inputTextList); // Сдвигаем влево последоватьельность на r 
            List<int> inputTextCopy = CreateCopy(shiftLeft); // Создаем копию shiftLeft, потому что лист - ссылочный тип данных и он меняется, а shiftLeft еще будет нужна
            List<int> xorWithGX = new();
            do
            {
                xorWithGX = XorWithGX(shiftLeft, gx);
            } while (xorWithGX.Count >= gx.Count); // Ксорим пока значение не станет короче полинома

            return CalculateCodeCombination(inputTextCopy, xorWithGX); // Добавлем к shiftLeft наше поксоренное значение
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

        private List<int> ShiftLeft(List<int> inputText)
        {
            for (int i = 0; i < 3; i++) inputText.Add(0);

            return inputText;
        }

        private List<int> CreateCopy(List<int> inputText)
        {
            List<int> result = new List<int>();
            foreach (int i in inputText)
            {
                result.Add(i);
            }
            return result;
        }

        private List<int> XorWithGX(List<int> shiftInputText, List<int> gx)
        {
            for (int i = 0; i < gx.Count; i++)
            {
                switch (shiftInputText[i] + gx[i]) // Xor
                {
                    case 0:
                        shiftInputText[i] = 0;
                        break;
                    case 1:
                        shiftInputText[i] = 1;
                        break;
                    case 2:
                        shiftInputText[i] = 0;
                        break;
                }
            }
            shiftInputText.RemoveAt(0); // Удаление первого бита, потому что мы понижаем степень

            return shiftInputText;
        }

        private List<int> CalculateCodeCombination(List<int> inputText, List<int> xorWithGX)
        {
            for (int i = xorWithGX.Count - 1, j = 1; i >= 0; i--, j++)
            {
                switch (inputText[inputText.Count - j] + xorWithGX[i]) // Xor
                {
                    case 0:
                        inputText[inputText.Count - j] = 0;
                        break;
                    case 1:
                        inputText[inputText.Count - j] = 1;
                        break;
                    case 2:
                        inputText[inputText.Count - j] = 0;
                        break;
                }
            }

            return inputText;
        }
    }
}
