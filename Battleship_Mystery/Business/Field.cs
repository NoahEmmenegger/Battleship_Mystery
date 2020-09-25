using Battleship_Mystery.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship_Mystery.Business
{
    public class Field
    {
        public FieldStatus Status { get; set; }
        public bool IsShipField { get; set; }
    }
}
