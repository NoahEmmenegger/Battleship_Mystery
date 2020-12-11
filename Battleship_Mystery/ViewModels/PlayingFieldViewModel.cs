using Battleship_Mystery.Business;
using Battleship_Mystery.Business.Service;
using Battleship_Mystery.GUI;
using Battleship_Mystery.GUI.Pages;
using Battleship_Mystery.ViewModels.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        public BackCommand BackCommand { get; set; }
        public LoadVirtualMysteryCommand LoadVirtualMysteryCommand { get; set; }
        public SafePDFCommand SafePDFCommand { get; set; }
        public SafePDFSolutionCommand SafePDFSolutionCommand { get; set; }

        public PlayingFieldViewModel(Mystery mystery)
        {
            Mystery = mystery;

            HelpCommand = new HelpCommand(GetHelp);
            CorrectCommand = new CorrectCommand(CorrectMystery);
            BackCommand = new BackCommand(Back);
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
            Mystery.DiscoverShipField();
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
            SaveLoadService.Save(Mystery);
        }

        private void Back(object parameter)
        {
            MainWindow main = MainWindow.GetSingelton();
            main.Show();
            main.ShowMysteryConfiguration();
        }

        private void LoadVirtualMystery(object parameter)
        {
            Mystery = SaveLoadService.Load(Mystery);

            MainWindow mainWindow = MainWindow.GetSingelton();
            mainWindow.Show();
            mainWindow.UpdatePlayingField(this);

        }

        private void SavePDF(object parameter)
        {

        }

        private void SavePDFSolution(object parameter)
        {

        }
    }
}
