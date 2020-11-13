using Battleship_Mystery.Business;
using Battleship_Mystery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Battleship_Mystery.GUI.Pages
{
    /// <summary>
    /// Interaktionslogik für PlayingField.xaml
    /// </summary>
    public partial class PlayingField : Page
    {
        public PlayingField(PlayingFieldViewModel viewModel)
        {
            InitializeComponent();

            for (int j = 0; j <= viewModel.Mystery.MysteryCreator.NumberOfRows; j++)
            {
                ColumnDefinition gridCol = new ColumnDefinition();
                DynamicGrid.ColumnDefinitions.Add(gridCol);
                gridCol.Width = new GridLength(50);
            }

            for (int i = 0; i <= viewModel.Mystery.MysteryCreator.NumberOfShips; i++)
            {
                RowDefinition gridRow = new RowDefinition();
                DynamicGrid.RowDefinitions.Add(gridRow);
                gridRow.Height = new GridLength(50);
            }

            for (int y = 1; y <= viewModel.Mystery.MysteryCreator.NumberOfColumns; y++)
            {
                for (int x = 1; x <= viewModel.Mystery.MysteryCreator.NumberOfRows; x++)
                {
                    Button newButton = new Button();

                    var test = viewModel.Mystery.FieldList.Where(field => field.XCoordinate == x && field.YCoordinate == y).First();

                    if(test.IsShipField)
                    {
                        var no = test.IsShipField;
                    }

                    newButton.Content = test.IsShipField?"test":null;
                    Grid.SetRow(newButton, y);
                    Grid.SetColumn(newButton, x);
                    DynamicGrid.SetValue(Grid.ColumnProperty, y);
                    DynamicGrid.SetValue(Grid.RowProperty, x);
                    DynamicGrid.Children.Add(newButton);
                }
            }



            
            DataContext = viewModel;
        }
    }
}
