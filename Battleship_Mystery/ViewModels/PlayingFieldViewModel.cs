using Battleship_Mystery.Business;
using Battleship_Mystery.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship_Mystery.ViewModels
{
    public class PlayingFieldViewModel
    {
        public ClickFieldCommand ClickFieldCommand { get; set; }

        public Mystery Mystery { get; set; }

        public PlayingFieldViewModel(Mystery mystery)
        {
            Mystery = mystery;
            ClickFieldCommand = new ClickFieldCommand(ClickField);
        }

        public void ClickField(object parameter)
        {

        }
    }
}
