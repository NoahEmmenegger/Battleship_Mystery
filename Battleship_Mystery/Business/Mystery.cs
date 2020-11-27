using Battleship_Mystery.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship_Mystery.Business
{
    public class Mystery
    {
        public List<Ship> ShipList { get; set; }
        public List<Field> FieldList { get; set; }

        public MysteryCreator MysteryCreator { get; set; }

        public Field GetTip()
        {
            return null;
        }

        public Mystery(MysteryCreator mysteryCreator)
        {
            MysteryCreator = mysteryCreator;
        }

        public void DiscoverShipField()
        {
            Field field = FieldList.Where(f => f.IsShipField && f.Status != FieldStatus.Ship).First();
            field.Status = FieldStatus.Ship;
        }
        

        public bool CeckCorrectness()
        {
            return false;
        }
    }
}
