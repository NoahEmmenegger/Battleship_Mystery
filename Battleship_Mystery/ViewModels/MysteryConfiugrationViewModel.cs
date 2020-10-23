using Battleship_Mystery.Business;
using Battleship_Mystery.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship_Mystery.ViewModels
{
    public class MysteryConfiugrationViewModel : PropertyChangedClass
    {
        private MysteryCreator _mysteryCreator = new MysteryCreator();

        public GenerateMysteryCommand GenerateMysteryCommand { get; private set; }

        public MysteryConfiugrationViewModel()
        {
            GenerateMysteryCommand = new GenerateMysteryCommand(GenerateMystery);
        }

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

        public void GenerateMystery(object parameter)
        {
            _mysteryCreator.Create();
        }
    }
}
