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

                CountInputText = _inputText.Length;
            }
        }

        private string _codeCombination;
        public string CodeCombination
        {
            get
            {
                return _codeCombination;
            }
            set
            {
                Set(ref _codeCombination, value);

                CountErrorText = _codeCombination.Length;
            }
        }

        private string _errorText;
        public string ErrorText
        {
            get
            {
                return _errorText;
            }
            set
            {
                Set(ref _errorText, value);

                _errorText = "1111001";
                ErrorCode errorCode = new ErrorCode();
                int a = errorCode.GetIndexError(_errorText, _gX);
            }
        }

        private int _countInputText;
        public int CountInputText { get => _countInputText; set => Set(ref _countInputText, value); }

        private int _countErrorText;
        public int CountErrorText { get => _countErrorText; set => Set(ref _countErrorText, value); }


        private List<int> _gX = new List<int>()
        {
            //1,0,0,0,  0,1,1,0,  1,1,1,0,  1,0,0,0,  0,0,0,1,  0,0,0,1,  0,0,1,1,
            1,0,1,1
        };



        public ICommand GetCodeCombinationCommand { get; }

        private bool CanCodeCombinationCommand(object p)
        {
            if (CountInputText == 4)
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
