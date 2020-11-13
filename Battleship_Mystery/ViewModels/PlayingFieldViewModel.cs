using Battleship_Mystery.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship_Mystery.ViewModels
{
    public class PlayingFieldViewModel
    {
        public Mystery Mystery { get; set; }

        public PlayingFieldViewModel(Mystery mystery)
        {
            Mystery = mystery;
        }
    }
}
