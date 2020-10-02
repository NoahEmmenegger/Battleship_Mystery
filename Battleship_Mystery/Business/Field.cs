using Battleship_Mystery.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship_Mystery.Business
{
    public class Field
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public FieldStatus Status { get; set; }
        public bool IsShipField { get; set; }

        public Field(int x, int y)
        {
            XCoordinate = x;
            YCoordinate = y;
        }
    }
}
