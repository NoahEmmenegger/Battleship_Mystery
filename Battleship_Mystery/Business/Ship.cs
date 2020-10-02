using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship_Mystery.Business
{
    public class Ship
    {
        public int Size { get; set; }
        public List<Field> Fields { get; set; }

        public Ship(int size)
        {
            Size = size;
        }
    }
}
