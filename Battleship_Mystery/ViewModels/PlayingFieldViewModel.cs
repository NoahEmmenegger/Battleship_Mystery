using Battleship_Mystery.Business;
using Battleship_Mystery.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Battleship_Mystery.ViewModels
{
    public class PlayingFieldViewModel
    {

        public Mystery Mystery { get; set; }

        public HelpCommand HelpCommand { get; set; }
        public CorrectCommand CorrectCommand { get; set; }
        public SaveVirtualMysteryCommand SaveVirtualMysteryCommand { get; set; }
        public LoadVirtualMysteryCommand LoadVirtualMysteryCommand { get; set; }
        public SafePDFCommand SafePDFCommand { get; set; }
        public SafePDFSolutionCommand SafePDFSolutionCommand { get; set; }

        public PlayingFieldViewModel(Mystery mystery)
        {
            Mystery = mystery;

            HelpCommand = new HelpCommand(GetHelp);
            CorrectCommand = new CorrectCommand(CorrectMystery);
            SaveVirtualMysteryCommand = new SaveVirtualMysteryCommand(SaveVirtualMystery);
            LoadVirtualMysteryCommand = new LoadVirtualMysteryCommand(LoadVirtualMystery);
            SafePDFCommand = new SafePDFCommand(SavePDF);
            SafePDFSolutionCommand = new SafePDFSolutionCommand(SavePDFSolution);
        }

        public void DiscoverField()
        {
           
        }

        private void GetHelp(object parameter)
        {

        }

        public void CorrectMystery(object parameter)
        {
            foreach (Field field in Mystery.FieldList)
            {
                if(field.Status == Enum.FieldStatus.Unexplored)
                {
                    MessageBox.Show("Es gibt noch Fehler", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if(field.Status == Enum.FieldStatus.Ship && !field.IsShipField)
                {
                    MessageBox.Show("Es gibt noch Fehler", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if(field.Status == Enum.FieldStatus.Water && field.IsShipField)
                {
                    MessageBox.Show("Es gibt noch Fehler", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            MessageBox.Show("Das Mystery wurde korrekt gelöst!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
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
