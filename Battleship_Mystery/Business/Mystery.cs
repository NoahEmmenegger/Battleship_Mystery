using Battleship_Mystery.Enum;
using Microsoft.Win32;
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

        public void DiscoverStartFields()
        {

            double countedShips = Convert.ToDouble(FieldList.Where(f => f.IsShipField && f.Status != FieldStatus.Ship).ToList().Count()) / 100 * 10;
            double countedWaterField = Convert.ToDouble(FieldList.Where(f => !f.IsShipField && f.Status != FieldStatus.Water).ToList().Count()) / 100 * 5;
            for (int i = 0; i < countedShips; i++)
            {
                DiscoverShipField();
            }
            for (int i = 0; i < countedWaterField; i++)
            {
                DiscoverWaterField();
            }
        }

        public void DiscoverShipField()
        {
            Random random = new Random();

            List<Field> shipList = FieldList.Where(f => f.IsShipField && f.Status != FieldStatus.Ship).ToList();
            if(shipList.Count > 0)
            {
                int randomIndex = random.Next(0, shipList.Count());

                Field field = shipList[randomIndex];
                field.Status = FieldStatus.Ship;
            }
        }

        public void DiscoverWaterField()
        {
            Random random = new Random();

            List<Field> waterList = FieldList.Where(f => !f.IsShipField && f.Status != FieldStatus.Water).ToList().ToList();
            if (waterList.Count > 0)
            {
                int randomIndex = random.Next(0, waterList.Count());

                Field field = waterList[randomIndex];
                field.Status = FieldStatus.Water;
            }
        }
        

        public bool CeckCorrectness()
        {
            return false;
        }
    }
}
