using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship_Mystery.Business
{
    public class Ship
    {
        /// <summary>
        /// Die Anzahl Felder, welche das Schiff einnimt.
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Die Felder von diesem Schiff
        /// </summary>
        public List<Field> Fields { get; set; }

        public Ship(int size)
        {
            Size = size;
        }
    }
}
