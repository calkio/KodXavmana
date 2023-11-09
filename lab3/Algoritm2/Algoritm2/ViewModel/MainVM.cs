using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritm2.ViewModel
{
    internal class MainVM: BaseVM
    {
        private string _inputText;
        public string InputText { get => _inputText; set => Set(ref _inputText, value); }

        private string _gX;
        public string GX { get => _gX; set => Set(ref _gX, value); }

        private string _errorText;
        public string ErrorText { get => _errorText; set => Set(ref _errorText, value); }
    }
}
