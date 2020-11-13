using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Battleship_Mystery.Business
{
    public class MysteryCreator : PropertyChangedClass
    {
        private int numberOfColumns = 2;
        private int numberOfRows = 2;
        private int numberOfShips = 1;

        public int NumberOfColumns
        {
            get
            {
                return numberOfColumns;
            }
            set
            {
                numberOfColumns = value;
                while ((numberOfColumns + 1) * (NumberOfRows + 1) < NumberOfShips * 9)
                {
                    numberOfShips--; 
                    OnPropertyChanged(nameof(NumberOfShips));
                }
                OnPropertyChanged();
            }
        }

        public int NumberOfRows
        {
            get
            {
                return numberOfRows;
            }
            set
            {
                numberOfRows = value;
                while ((NumberOfColumns + 1) * (numberOfRows + 1) < NumberOfShips * 9)
                {
                    numberOfShips--;
                    OnPropertyChanged(nameof(NumberOfShips));
                }
                OnPropertyChanged();
            }
        }
        public int NumberOfShips
        {
            get
            {
                return numberOfShips;
            }
            set
            {
                if((NumberOfColumns + 1) * (NumberOfRows + 1) >= value * 9)
                {
                    numberOfShips = value;
                    OnPropertyChanged();
                }
            }
        }

        public Mystery Create()
        {
            Mystery mystery = new Mystery();

            //init Fields and add to mystery
            List<Field> fieldList = GetFieldList();
            List<Ship> shipList = GetShips();

            //init Ships and add to mystery
            foreach (Ship ship in shipList)
            {
                Field randomFieldRoot = GetRandomValidField(fieldList);


                //save root field
                GetFieldFromCoordinate(randomFieldRoot.XCoordinate, randomFieldRoot.YCoordinate, fieldList).IsShipField = true;
            }

            mystery.FieldList = fieldList;
            mystery.ShipList = shipList;


            return mystery;
        }

        protected Field GetRandomValidField(List<Field> fields)
        {
            Field randomField = null;
            Random random = new Random();
            while (randomField == null || !HasFreeFieldsArround(randomField, fields))
            {
                int randomItem = random.Next(1, fields.Count + 1);
                if(!fields[randomItem].IsShipField)
                {
                    randomField = fields[randomItem];
                }
            }
            
            return randomField;

        }

        protected bool HasFreeFieldsArround(Field field, List<Field> fields)
        {
            if (field == null) return true;
            foreach (Field fieldItem in GetFieldsArround(field, fields))
            {
                // Wenn field rand oder wasser ist, soll nichts passieren. Wenn es ein Shipfield ist dann soll er false returnen.
                if (field != null)
                {
                    if (field.IsShipField)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected List<Field> GetFieldsArround(Field field, List<Field> fields)
        {
            return new List<Field>()
            {
                GetFieldFromCoordinate(field.XCoordinate, field.YCoordinate - 1, fields),
                GetFieldFromCoordinate(field.XCoordinate - 1, field.YCoordinate - 1, fields),
                GetFieldFromCoordinate(field.XCoordinate - 1, field.YCoordinate, fields),
                GetFieldFromCoordinate(field.XCoordinate - 1, field.YCoordinate + 1, fields),
                GetFieldFromCoordinate(field.XCoordinate, field.YCoordinate + 1, fields),
                GetFieldFromCoordinate(field.XCoordinate + 1, field.YCoordinate + 1, fields),
                GetFieldFromCoordinate(field.XCoordinate + 1, field.YCoordinate, fields),
                GetFieldFromCoordinate(field.XCoordinate + 1, field.YCoordinate - 1, fields)
            };
        }

        protected Field GetFieldFromCoordinate(int x, int y, List<Field> fields)
        {
            return fields.Find(field => field.XCoordinate == x && field.YCoordinate == y);
        }

        protected int GetRandomShipCountFromNumerOfShips(int availableCount)
        {
            Random random = new Random();
            int maxShipSize = 5;
            if(availableCount <= 5)
            {
                maxShipSize = availableCount;
            }
            return random.Next(1, maxShipSize +1);
        }

        protected List<Field> GetFieldList()
        {
            List<Field> fields = new List<Field>();
            for (int y =1;y <= NumberOfColumns; y++)
            {
                for (int x = 1; x <= NumberOfRows; x++)
                {
                    fields.Add(new Field(x, y));
                }
            }
            return fields;
        }

        protected List<int> GetShipSizes()
        {
            int availableShipCount = NumberOfShips;
            List<int> shipSizes = new List<int>();
            while (availableShipCount > 0)
            {
                int randomShipSize = GetRandomShipCountFromNumerOfShips(availableShipCount);
                shipSizes.Add(randomShipSize);
                availableShipCount -= randomShipSize;
            }
            return shipSizes;
        }

        protected List<Ship> GetShips()
        {
            List<Ship> ships = new List<Ship>();
            List<int> shipSizes = GetShipSizes();
            foreach (int shipSize in shipSizes)
            {
                Ship newShip = new Ship(shipSize);
                ships.Add(newShip);
            }
            return ships;
        }
    }
}
