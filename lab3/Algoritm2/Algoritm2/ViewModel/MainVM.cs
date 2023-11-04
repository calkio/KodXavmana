using Algoritm2.Model;
using Algoritm2.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        public ICommand GetqweCommand { get; }

        private bool CanqweCommand(object p)
        {
            return true;
        }

        private void OnqweCommand(object p)
        {
            Xor xor = new Xor(_gX);
        }

        MainVM()
        {
            GetqweCommand = new LambdaCommand(OnqweCommand, CanqweCommand);
        }
    }
}
