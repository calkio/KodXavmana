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

            Dictionary<char, double> frequencyTable = new Dictionary<char, double>();
            foreach (var kvp in letterCounts)
            {
                double probability = kvp.Value / totalLetters;
                frequencyTable[kvp.Key] = probability;
            }

            var result = frequencyTable.OrderByDescending(kv => kv.Value)
                                       .ToDictionary(kv => kv.Key, kv => kv.Value);

            //Dictionary<char, double> letterCounts = new Dictionary<char, double>
            //{
            //    {'о', 0.25 },
            //    {'ф', 0.2 },
            //    {'а', 0.15 },
            //    {'т', 0.15 },
            //    {' ', 0.1 },
            //    {'г', 0.05 },
            //    {'л', 0.05 },
            //    {'р', 0.05 },
            //};

            return result;
        }
    }
}
