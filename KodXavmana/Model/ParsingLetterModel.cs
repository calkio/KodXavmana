using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KodXavmana.ViewModel;


namespace KodXavmana.model
{
    internal class ParsingLetterModel
    {

        public static Dictionary<char, double> CalculateLetterProbabilities(string inputText)
        {
            Dictionary<char, double> letterCounts = new Dictionary<char, double>();
            int totalLetters = 0;

            foreach (char c in inputText)
            {
                char letter = char.ToLower(c);
                if (letterCounts.ContainsKey(letter))
                {
                    letterCounts[letter]++;
                }
                else
                {
                    letterCounts[letter] = 1;
                }
                totalLetters++;
            }

            Dictionary<char, double> letterProbabilities = new Dictionary<char, double>();
            foreach (var kvp in letterCounts)
            {
                double probability = kvp.Value / totalLetters;
                letterProbabilities[kvp.Key] = probability;
            }

            return letterProbabilities;
        }
    }
}
