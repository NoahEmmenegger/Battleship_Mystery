using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Battleship_Mystery.ViewModels.Commands
{
    public class GenerateMysteryCommand : BaseCommand
    {
        public GenerateMysteryCommand(Action<object> execute) : base(execute)
        {

        }
    }
}
