using Algoritm2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Algoritm2.ViewModel.Command
{
    internal class ViewCommands
    {
        public ICommand GetqweCommand { get; }

        private bool CanqweCommand(object p)
        {
            return true;
        }

        private void OnqweCommand(object p)
        {
            Xor xor = new Xor(_gX);
        }

        public ViewCommands(ICommand getqweCommand)
        {
            GetqweCommand = new LambdaCommand(OnqweCommand, CanqweCommand);
        }
    }
}
