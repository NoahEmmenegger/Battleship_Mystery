using Battleship_Mystery.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Battleship_Mystery.ViewModels
{
    public class MainViewModel : PropertyChangedClass
    {
        private MysteryCreator _mysteryCreator = new MysteryCreator();
        public MysteryCreator MysteryCreator
        {
            get
            {
                return _mysteryCreator;
            }
            set
            {
                _mysteryCreator = value;
                OnPropertyChanged();
            }
        }
    }
}
