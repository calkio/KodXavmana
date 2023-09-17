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



        private Dictionary<char, double> _letterProbabilities;

        public Dictionary<char, double> LetterProbabilities { get => _letterProbabilities; set => Set(ref _letterProbabilities, value); }

        #endregion

        public ICommand GetTableLetterProbabilitiesCommand { get; }

        private bool CanGetTableLetterProbabilitiesCommand(object p)
        {
            // Проверяем, содержит ли _inputText данные
            return !string.IsNullOrWhiteSpace(_inputText);
        }

        private void OnGetTableLetterProbabilitiesCommand(object p)
        {
            var dict = ParsingLetterModel.CalculateLetterProbabilities(_inputText);

            TableLetter = string.Join(Environment.NewLine, dict.Select(kv => $"{kv.Key} - {Math.Round(kv.Value, 2)}"));
        }

        public MainViewModel()
        {
            GetTableLetterProbabilitiesCommand = new LambdaCommand(OnGetTableLetterProbabilitiesCommand, CanGetTableLetterProbabilitiesCommand);
        }
    }
}
