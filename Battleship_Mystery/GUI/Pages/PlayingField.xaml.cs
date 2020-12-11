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

            this.ShowsNavigationUI = false;

            for (int j = 0; j <= viewModel.Mystery.MysteryCreator.NumberOfColumns + 1; j++)
            {
                ColumnDefinition gridCol = new ColumnDefinition();
                gridCol.Width = new GridLength(50);
                DynamicGrid.ColumnDefinitions.Add(gridCol);
            }

            for (int i = 0; i <= viewModel.Mystery.MysteryCreator.NumberOfRows + 1; i++)
            {
                RowDefinition gridRow = new RowDefinition();
                gridRow.Height = new GridLength(50);
                DynamicGrid.RowDefinitions.Add(gridRow);
            }

            int[] shipColumnCounter = new int[viewModel.Mystery.MysteryCreator.NumberOfColumns];
            int[] shipRowCounter = new int[viewModel.Mystery.MysteryCreator.NumberOfRows];

            for (int y = 1; y <= viewModel.Mystery.MysteryCreator.NumberOfColumns; y++)
            {
                for (int x = 1; x <= viewModel.Mystery.MysteryCreator.NumberOfRows; x++)
                {
                    Button newButton = new Button();

                    var field = viewModel.Mystery.FieldList.Where(field => field.XCoordinate == x && field.YCoordinate == y).First();

                    if(field.IsShipField)
                    {
                        shipColumnCounter[y -1]++;
                        shipRowCounter[x -1]++;
                        var no = field.IsShipField;
                    }

                    // Todo: Remove
                    //newButton.Content = field.IsShipField ? "x" : null;

                    //newButton.Content = field.IsShipField ? "test" : null;
                    Binding bindingContent = new Binding("StatusColor");
                    bindingContent.Source = field;
                    newButton.SetBinding(Button.BackgroundProperty, bindingContent);

                    Binding bindingCommand = new Binding("ClickFieldCommand");
                    bindingCommand.Source = field;
                    newButton.SetBinding(Button.CommandProperty, bindingCommand);


                    Grid.SetRow(newButton, y);
                    Grid.SetColumn(newButton, x);
                    DynamicGrid.SetValue(Grid.ColumnProperty, y);
                    DynamicGrid.SetValue(Grid.RowProperty, x);
                    DynamicGrid.Children.Add(newButton);
                }
            }

            for (int y = 0; y < shipColumnCounter.Length; y++)
            {
                Label newLabel = new Label();
                newLabel.Content = shipColumnCounter[y];

                Grid.SetRow(newLabel, y+1);
                Grid.SetColumn(newLabel, 0);
                DynamicGrid.SetValue(Grid.ColumnProperty, y);
                DynamicGrid.SetValue(Grid.RowProperty, 0);
                DynamicGrid.Children.Add(newLabel);
            }

            for (int x = 0; x < shipRowCounter.Length; x++)
            {
                Label newLabel = new Label();
                newLabel.Content = shipRowCounter[x];

                Grid.SetRow(newLabel, 0);
                Grid.SetColumn(newLabel, x + 1);
                DynamicGrid.SetValue(Grid.ColumnProperty, 0);
                DynamicGrid.SetValue(Grid.RowProperty, x + 1);
                DynamicGrid.Children.Add(newLabel);
            }

            DataContext = viewModel;

            int counter = 0;
            foreach (Ship ship in viewModel.Mystery.ShipList)
            {
                RowDefinition gridCol = new RowDefinition();
                ShipsGrid.RowDefinitions.Add(gridCol);

                DockPanel stackPanel = new DockPanel();

                for (int i = 0; i < ship.Size; i++)
                {
                    Label label = new Label();
                    label.Content = "x";
                    Grid.SetRow(stackPanel, counter);
                    Grid.SetColumn(stackPanel, 1);
                    stackPanel.Children.Add(label);
                }
                ShipsGrid.Children.Add(stackPanel);
                counter++;
            }
        }
    }
}
