using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Battleship_Mystery.Business.Service
{
    public static class SaveLoadService
    {
        /// <summary>
        /// Lade ein mystery
        /// </summary>
        /// <param name="mystery"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Mystery Load(Mystery mystery, string filename)
        {

                mystery.FieldList.Clear();
                mystery.ShipList.Clear();
                using (var streamReader = File.OpenText(filename))
                {
                    var lines = streamReader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    foreach (var line in lines)
                    {
                        var arguments = line.Split(":");
                        if (arguments[0] == "s")
                        {
                            Ship ship = new Ship(Convert.ToInt32(arguments[1]));
                            mystery.ShipList.Add(ship);

                        }
                        else if (arguments[0] == "c")
                        {
                            if (arguments[1] == "y")
                            {
                                mystery.MysteryCreator.NumberOfColumns = Convert.ToInt32(arguments[2]);
                            }
                            if (arguments[1] == "x")
                            {
                                mystery.MysteryCreator.NumberOfRows = Convert.ToInt32(arguments[2]);
                            }
                        }
                        else
                        {
                            Field field = new Field(Convert.ToInt32(arguments[0]), Convert.ToInt32(arguments[1]));
                            field.Status = (Enum.FieldStatus)Convert.ToInt32(arguments[2]);
                            field.IsShipField = Convert.ToBoolean(arguments[3]);
                            mystery.FieldList.Add(field);
                        }

                    }
                }
            return mystery;
        }

        /// <summary>
        /// Speichere ein Mystery
        /// </summary>
        /// <param name="mystery"></param>
        /// <param name="filename"></param>
        public static void Save(Mystery mystery, string filename)
        {
            string text = "";
            foreach (Field field in mystery.FieldList)
            {
                text += field.XCoordinate + ":" + field.YCoordinate + ":" + (int)field.Status + ":" + field.IsShipField + "\r";
            }
            foreach (Ship ship in mystery.ShipList)
            {
                text += "s:" + ship.Size + "\r";
            }
            text += "c:x:" + mystery.MysteryCreator.NumberOfRows + "\r";
            text += "c:y:" + mystery.MysteryCreator.NumberOfColumns + "\r";
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(text);
            }
        }
    }
}
