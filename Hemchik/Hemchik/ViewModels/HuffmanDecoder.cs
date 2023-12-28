using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemchik.ViewModels
{
    internal class HuffmanDecoder
    {
        private Dictionary<char, string> huffmanDictionary;

        public HuffmanDecoder(Dictionary<char, string> huffmanDictionary)
        {
            this.huffmanDictionary = huffmanDictionary;
        }

        public string Decode(string encodedData)
        {
            string decodedData = "";
            string currentCode = "";

            foreach (char bit in encodedData)
            {
                currentCode += bit.ToString();

                if (huffmanDictionary.ContainsValue(currentCode))
                {
                    char decodedChar = huffmanDictionary.FirstOrDefault(x => x.Value == currentCode).Key;
                    decodedData += decodedChar;
                    currentCode = "";
                }
            }

            return decodedData;
        }
    }
}
