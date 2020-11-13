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
        public SafePDFCommand SafePDFCommand { get; set; }
        public SafePDFSolutionCommand SafePDFSolutionCommand { get; set;}

        public ToolMenuViewModel()
        {
            HelpCommand = new HelpCommand(GetHelp);
            SaveVirtualMysteryCommand = new SaveVirtualMysteryCommand(SaveVirtualMystery);
            LoadVirtualMysteryCommand = new LoadVirtualMysteryCommand(LoadVirtualMystery);
            SafePDFCommand = new SafePDFCommand(SavePDF);
            SafePDFSolutionCommand = new SafePDFSolutionCommand(SavePDFSolution);
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

        private void SavePDF(object parameter)
        {

        }

        private void SavePDFSolution(object parameter)
        {

        }
    }
}
