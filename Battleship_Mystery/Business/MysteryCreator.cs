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
                while ((numberOfColumns + 1) * (NumberOfRows + 1) < NumberOfShips * 5)
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
                while ((NumberOfColumns + 1) * (numberOfRows + 1) < NumberOfShips * 5)
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
                if((NumberOfColumns + 1) * (NumberOfRows + 1) >= value * 5)
                {
                    numberOfShips = value;
                    OnPropertyChanged();
                }
                else
                {
                    if(value > 1)
                    {
                        NumberOfShips = value - 1;
                    }
                }
            }
        }

        public Mystery Create()
        {
            Mystery mystery = new Mystery(this);

            //init Fields and add to mystery
            List<Field> fieldList = GetFieldList();
            List<Ship> shipList = GetShips();

            //init Ships and add to mystery
            foreach (Ship ship in shipList)
            {
                List<Field> randomFieldRoots = new List<Field>();
                while (randomFieldRoots.Count == 0)
                {
                    randomFieldRoots = GetRandomValidField(fieldList, ship.Size);
                }

                foreach (Field randomFieldRoot in randomFieldRoots)
                {
                    //save root field
                    GetFieldFromCoordinate(randomFieldRoot.XCoordinate, randomFieldRoot.YCoordinate, fieldList).IsShipField = true;
                }
            }

            mystery.FieldList = fieldList;
            mystery.ShipList = shipList;
            mystery.DiscoverStartFields();

            return mystery;
        }

        protected List<Field> GetRandomValidField(List<Field> fields, int shipSize)
        {
            Field randomField = null;
            Random random = new Random();
            //Finde ein Feld solange bis ein valides Feld gefunden wurde

            while (randomField == null || !HasFreeFieldsArround(randomField, fields))
            {
                int randomItem = random.Next(0, fields.Count);
                if(!fields[randomItem].IsShipField)
                {
                    randomField = fields[randomItem];
                }
            }
            return GetValidFieldsByShipLenght(randomField, shipSize, fields);

        }

        protected List<Field> GetValidFieldsByShipLenght(Field field, int size, List<Field> fields)
        {
            if (field == null) return null;
            List<Field> upperFields = GetFieldsByDirection(field, size, fields, "up");
            List<Field> leftFields = GetFieldsByDirection(field, size, fields, "left");
            List<Field> downFields = GetFieldsByDirection(field, size, fields, "down");
            List<Field> rightFields = GetFieldsByDirection(field, size, fields, "right");

            List<List<Field>> validFields = new List<List<Field>>();
            if (AreFieldsAvailable(upperFields, fields)) validFields.Add(upperFields);
            if (AreFieldsAvailable(leftFields, fields)) validFields.Add(leftFields);
            if (AreFieldsAvailable(downFields, fields)) validFields.Add(downFields);
            if (AreFieldsAvailable(rightFields, fields)) validFields.Add(rightFields);

            if (validFields.Count <= 0) return new List<Field>();


            Random random = new Random();
            int index = random.Next(0, validFields.Count);
            return validFields[index];
        }

        protected bool AreFieldsAvailable(List<Field> fields, List<Field> allFields)
        {
            foreach (Field field in fields)
            {
                if (field == null) return false;
                if (field.IsShipField) return false;

                if (!HasFreeFieldsArround(field, allFields)) return false;
            }
            return true;
        }

        protected List<Field> GetFieldsByDirection(Field field, int size, List<Field> fields, string direction)
        {
            List<Field> upperFields = new List<Field>();
            upperFields.Add(field);
            for (int i = 1; i < size; i++)
            {
                if(direction == "up") upperFields.Add(GetFieldAbove(upperFields[upperFields.Count - 1], fields));
                if(direction == "left") upperFields.Add(GetFieldLeft(upperFields[upperFields.Count - 1], fields));
                if(direction == "down") upperFields.Add(GetFieldDown(upperFields[upperFields.Count - 1], fields));
                if(direction == "right") upperFields.Add(GetFieldRight(upperFields[upperFields.Count - 1], fields));
            }
            return upperFields;
        }

        protected Field GetFieldAbove(Field field, List<Field> fields)
        {
            if (field == null) return null;
            return GetFieldFromCoordinate(field.XCoordinate, field.YCoordinate - 1, fields);
        }

        protected Field GetFieldLeft(Field field, List<Field> fields)
        {
            if (field == null) return null;
            return GetFieldFromCoordinate(field.XCoordinate -1, field.YCoordinate, fields);
        }

        protected Field GetFieldDown(Field field, List<Field> fields)
        {
            if (field == null) return null;
            return GetFieldFromCoordinate(field.XCoordinate, field.YCoordinate + 1, fields);
        }

        protected Field GetFieldRight(Field field, List<Field> fields)
        {
            if (field == null) return null;
            return GetFieldFromCoordinate(field.XCoordinate + 1, field.YCoordinate, fields);
        }

        protected bool HasFreeFieldsArround(Field field, List<Field> fields)
        {
            if (field == null) return true;
            foreach (Field fieldItem in GetFieldsArround(field, fields))
            {
                // Wenn field rand oder wasser ist, soll nichts passieren. Wenn es ein Shipfield ist dann soll er false returnen.
                if (fieldItem != null)
                {
                    if (fieldItem.IsShipField)
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
