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
        private string _inputText = "";
        public string InputText
        {
            get
            {
                return _inputText;
            }
            set
            {
                Set(ref _inputText, value);

                Count = _inputText.Length;
            }
        }

        private string _codeCombination;
        public string CodeCombination { get => _codeCombination; set => Set(ref _codeCombination, value); }

        private string _errorText;
        public string ErrorText { get => _errorText; set => Set(ref _errorText, value); }

        private int _count;
        public int Count { get => _count; set => Set(ref _count, value); }


        private List<int> _gX = new List<int>()
        {
            1,0,0,0,  0,1,1,0,  1,1,1,0,  1,0,0,0,  0,0,0,1,  0,0,0,1,  0,0,1,1,
        };



        public ICommand GetCodeCombinationCommand { get; }

        private bool CanCodeCombinationCommand(object p)
        {
            if (Count == 36)
            {
                return true;
            }
            return false;
        }

        private void OnCodeCombinationCommand(object p)
        {
            CodeCombination codeCombination = new CodeCombination();
            List<int> codeCombinationList = codeCombination.GetCodeCombination(_inputText, _gX);
            CodeCombination = "";
            foreach (var value in codeCombinationList) // Переводим из листа чисел в строку
            {
                CodeCombination += value.ToString();
            }
            ErrorText += CodeCombination;
        }



        public MainVM()
        {
            GetCodeCombinationCommand = new LambdaCommand(OnCodeCombinationCommand, CanCodeCombinationCommand);
        }
    }
}
