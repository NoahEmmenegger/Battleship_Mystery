using Battleship_Mystery.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship_Mystery.ViewModels
{
    public class ToolMenuViewModel
    {
        public HelpCommand HelpCommand { get; set; }
        public SaveVirtualMysteryCommand SaveVirtualMysteryCommand { get; set; }
        public LoadVirtualMysteryCommand LoadVirtualMysteryCommand { get; set; }

        public ToolMenuViewModel()
        {
            HelpCommand = new HelpCommand(GetHelp);
            SaveVirtualMysteryCommand = new SaveVirtualMysteryCommand(SaveVirtualMystery);
            LoadVirtualMysteryCommand = new LoadVirtualMysteryCommand(LoadVirtualMystery);

        }

        private void GetHelp(object parameter)
        {

        }

        private void SaveVirtualMystery(object parameter)
        {

        }

        private void LoadVirtualMystery(object parameter)
        {

        }
    }
}
