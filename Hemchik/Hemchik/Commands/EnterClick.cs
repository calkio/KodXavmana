using Hemchik.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hemchik;

namespace Hemchik.Commands
{
    class EnterClick: CommandBase
    {
        public override bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter) {}
    }
}
