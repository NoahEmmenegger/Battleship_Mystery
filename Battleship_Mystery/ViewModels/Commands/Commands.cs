using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship_Mystery.ViewModels.Commands
{
    public class GenerateMysteryCommand : BaseCommand
    {
        public GenerateMysteryCommand(Action<object> execute) : base(execute)
        {

        }
    }

    public class HelpCommand : BaseCommand
    {
        public HelpCommand(Action<object> execute) : base(execute)
        {

        }
    }

    public class SaveVirtualMysteryCommand : BaseCommand
    {
        public SaveVirtualMysteryCommand(Action<object> execute) : base(execute)
        {

        }
    }

    public class LoadVirtualMysteryCommand : BaseCommand
    {
        public LoadVirtualMysteryCommand(Action<object> execute) : base(execute)
        {

        }
    }

    public class SafePDFCommand : BaseCommand
    {
        public SafePDFCommand(Action<object> execute) : base(execute)
        {

        }
    }

    public class SafePDFSolutionCommand : BaseCommand
    {
        public SafePDFSolutionCommand(Action<object> execute) : base(execute)
        {

        }
    }


}
