using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KodXavmana.model;
using System.Windows.Input;
using System.Windows;
using System.Diagnostics;
using KodXavmana.Infastructure.Commands;
using KodXavmana.Model;

namespace KodXavmana.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        #region СВОЙСТВА
        private string _inputText;

        public string InputText { get => _inputText; set => Set(ref _inputText, value); }



        private string _outputText;

        public string OutputText { get => _outputText; set => Set(ref _outputText, value); }



        private string _tableLetter;

        public string TableLetter { get => _tableLetter; set => Set(ref _tableLetter, value); }

        private string _hx;

        public string Hx { get => _hx; set => Set(ref _hx, value); }

        private string _redundancy;

        public string Redundancy { get => _redundancy; set => Set(ref _redundancy, value); }


        #endregion

        #region КОМАНДЫ

        public ICommand GetTableLetterProbabilitiesCommand { get; }

        private bool CanGetTableLetterProbabilitiesCommand(object p)
        {
            // Проверяем, содержит ли _inputText данные
            return !string.IsNullOrWhiteSpace(_inputText);
        }

        private void OnGetTableLetterProbabilitiesCommand(object p)
        {
            var dict = ParsingLetterModel.CalculateLetterProbabilities(_inputText);
            Huffman huffman = new Huffman(dict);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in _inputText)
            {
                if (huffman.CodeTable.ContainsKey(c))
                {
                    stringBuilder.Append(huffman.CodeTable[c]);
                }
            }

            OutputText = stringBuilder.ToString();
            Hx = huffman.Hx.ToString();
            Redundancy = huffman.Redundancy.ToString();

            TableLetter = string.Join(Environment.NewLine, dict.Select(kv => $"{kv.Key} - {Math.Round(kv.Value, 2)}"));
        } 

        #endregion

        public MainViewModel()
        {
            GetTableLetterProbabilitiesCommand = new LambdaCommand(OnGetTableLetterProbabilitiesCommand, CanGetTableLetterProbabilitiesCommand);
        }
    }
}
