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
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = "export.txt";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.Filter = "txt files (*.txt) | *.txt";

                if (saveFileDialog.ShowDialog() == true)
                {
                    SaveLoadService.Save(Mystery, saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler beim Speichern", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back(object parameter)
        {
            MainWindow main = MainWindow.GetSingelton();
            main.Show();
            main.ShowMysteryConfiguration();
        }

        private void LoadVirtualMystery(object parameter)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    Mystery = SaveLoadService.Load(Mystery, openFileDialog.FileName);
                }

                MainWindow mainWindow = MainWindow.GetSingelton();
                mainWindow.Show();
                mainWindow.UpdatePlayingField(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler beim Laden", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SavePDF(object parameter)
        {
            try
            {
                SaveFileDialog openFileDialog = new SaveFileDialog();
                openFileDialog.FileName = "Mystery";
                openFileDialog.Filter = "Pdf Files|*.pdf";
                if (openFileDialog.ShowDialog() == true)
                {
                    ExportService.Export(Mystery, openFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler beim Speichern", MessageBoxButton.OK, MessageBoxImage.Error);
            }
    
        }

        private void SavePDFSolution(object parameter)
        {
            try
            {
                SaveFileDialog openFileDialog = new SaveFileDialog();
                openFileDialog.FileName = "Mystery Solution";
                openFileDialog.Filter = "Pdf Files|*.pdf";
                if (openFileDialog.ShowDialog() == true)
                {
                    ExportService.ExportSolution(Mystery, openFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler beim Speichern", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
