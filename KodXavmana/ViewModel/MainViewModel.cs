using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodXavmana.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        private string _inputText;

        public string InputText { get => _inputText; set => Set(ref _inputText, value); }


        private string _outputText;

        public string OutputText { get => _outputText; set => Set(ref _outputText, value); }

        private string _tableLetter;

        public string TableLetter { get => _tableLetter; set => Set(ref _tableLetter, value); }


    }
}
