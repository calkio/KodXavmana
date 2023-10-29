using Hemchik.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hemchik.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private string _inputMessage;
        public string inputMessage
        {
            get => _inputMessage;
            set { Set(ref _inputMessage, value); }
        }


        private string _outputMessage;
        public string outputMessage
        {
            get => _outputMessage;
            set => Set(ref _outputMessage, value);
        }
    }
}
