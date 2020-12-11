using Battleship_Mystery.GUI.Pages;
using Battleship_Mystery.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Battleship_Mystery.GUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow singelton;
        private MainWindow()
        {
            InitializeComponent();
            Main.Content = new MysteryConfiguration();
        }

        public static MainWindow GetSingelton()
        {
            if (singelton != null)
                return singelton;
            else
            {
                MainWindow mainWindow = new MainWindow();
                return singelton = mainWindow;
            }
        }

        public void ShowPlayingField(PlayingFieldViewModel viewModel)
        {
            Main.Content = new PlayingField(viewModel);
        }

        public void UpdatePlayingField(PlayingFieldViewModel viewModel)
        {
            Main.Content = new PlayingField(viewModel);
        }

        public void ShowMysteryConfiguration()
        {
            Main.Content = new MysteryConfiguration();
        }
    }
}
