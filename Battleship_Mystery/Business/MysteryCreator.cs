using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Battleship_Mystery.Business
{
    public class MysteryCreator : PropertyChangedClass
    {
        private int numberOfCollums;
        private int numberOfRows;
        private int numberOfShips;

        public int NumberOfCollumns
        {
            get
            {
                return numberOfCollums;
            }
            set
            {
                numberOfCollums = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfRows
        {
            get
            {
                return numberOfRows;
            }
            set
            {
                numberOfRows = value;
                OnPropertyChanged();
            }
        }
        public int NumberOfShips
        {
            get
            {
                return numberOfShips;
            }
            set
            {
                numberOfShips = value;
                OnPropertyChanged();
            }
        }

        public Mystery Create()
        {
            return null;
        }
    }
}
