using Battleship_Mystery.Enum;
using Battleship_Mystery.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Battleship_Mystery.Business
{
    public class Field : PropertyChangedClass
    {
        private FieldStatus _status;
        public ClickFieldCommand ClickFieldCommand { get; set; }

        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public FieldStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(StatusColor));
            }
        }
        public SolidColorBrush StatusColor
        {
            get
            {
                if (Status == FieldStatus.Unexplored)
                {
                    return new SolidColorBrush(Colors.Gray);
                }
                if (Status == FieldStatus.Water)
                {
                    return new SolidColorBrush(Colors.Blue);
                }
                return new SolidColorBrush(Colors.Orange);
            }
        }
        public bool IsShipField { get; set; }

        public Field(int x, int y)
        {
            XCoordinate = x;
            YCoordinate = y;
            ClickFieldCommand = new ClickFieldCommand(ChangeStatus);
        }

        public void ChangeStatus(object parameter)
        {
            if(Status == FieldStatus.Unexplored)
            {
                Status = FieldStatus.Water;
            }
            else if(Status == FieldStatus.Water)
            {
                Status = FieldStatus.Ship;
            }
            else
            {
                Status = FieldStatus.Unexplored;
            }
        }
    }
}
