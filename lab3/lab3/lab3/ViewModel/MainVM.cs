using lab3.Model;
using lab3.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace lab3.ViewModel
{
    internal class MainVM: BaseVM
    {
        private string _inputText;
        public string InputText { get => _inputText; set => Set(ref _inputText, value); }

        private string _errorText;
        public string ErrorText { get => _errorText; set => Set(ref _errorText, value); }


        private List<int> _gX = new List<int>()
        {
            //1,0,0,0,  0,1,1,0,  1,1,1,0,  1,0,0,0,  0,0,0,1,  0,0,0,1,  0,0,1,1,
            1,0,1,1
        };




        public ICommand GetCodeCombinationCommand { get; }

        private bool CanCodeCombinationCommand(object p)
        {
            return true;
        }

        private void OnCodeCombinationCommand(object p)
        {
            CodeCombination codeCombination = new CodeCombination();
            List<int> qwe = new List<int>();
            foreach (var item in _inputText)
            {
                qwe.Add((int)Char.GetNumericValue(item));
            }
            List<int> a = codeCombination.GetCodeCombination(qwe, _gX);
        }



        public MainVM()
        {
            GetCodeCombinationCommand = new LambdaCommand(OnCodeCombinationCommand, CanCodeCombinationCommand);
        }
    }
}
