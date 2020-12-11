using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Battleship_Mystery.Business.Service
{
    static class SaveLoadService
    {
        public static Mystery Load(Mystery mystery)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                mystery.FieldList.Clear();
                mystery.ShipList.Clear();
                using (var streamReader = File.OpenText(openFileDialog.FileName))
                {
                    var lines = streamReader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    foreach (var line in lines)
                    {
                        var arguments = line.Split(":");
                        if (arguments[0] == "s")
                        {
                            Ship ship = new Ship(Convert.ToInt32(arguments[1]));
                            mystery.ShipList.Add(ship);

                        }else
                        {
                            Field field = new Field(Convert.ToInt32(arguments[0]), Convert.ToInt32(arguments[1]));
                            field.Status = (Enum.FieldStatus)Convert.ToInt32(arguments[2]);
                            field.IsShipField = Convert.ToBoolean(arguments[3]);
                            mystery.FieldList.Add(field);
                        }

                    }
                }
            }
            return mystery;
        }

        public static void Save(Mystery mystery)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "export.txt";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "txt files (*.txt) | *.txt";
            string text = "";
            foreach (Field field in mystery.FieldList)
            {
                text += field.XCoordinate + ":" + field.YCoordinate + ":" + (int)field.Status + ":" + field.IsShipField + "\r";
            }
            foreach (Ship ship in mystery.ShipList)
            {
                text += "s:" + ship.Size + "\r";
            }
            text += "xconf:" + mystery.MysteryCreator.NumberOfColumns + "\r";
            text += "yconf:" + mystery.MysteryCreator.NumberOfRows + "\r";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.OpenFile()))
                {
                    sw.Write(text);
                }
            }
        }
    }
}
