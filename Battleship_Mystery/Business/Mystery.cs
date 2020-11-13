using System;
using System.Collections.Generic;
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

        public void ChangeFieldStatus(Field field)
        {

        }

        public bool CeckCorrectness()
        {
            return false;
        }
    }
}
