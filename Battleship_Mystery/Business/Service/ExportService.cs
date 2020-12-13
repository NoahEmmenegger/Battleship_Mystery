using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Documents;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;

namespace Battleship_Mystery.Business.Service
{
    public static class ExportService
    {
        public static void Export(Mystery mystery, string filePath)
        {
   
            Document doc = new Document();
            PdfPTable table = CreateTable(mystery, false);
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));

            doc.Open();           
            doc.Add(table);
            doc.Add(new Phrase(CreateShipInformations(mystery)));
            doc.Close();
        }

        public static void ExportSolution(Mystery mystery, string filePath)
        {
            Document doc = new Document();
            PdfPTable table = CreateTable(mystery, true);
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));

            doc.Open();
            doc.Add(table);
            doc.Close();
        }

        private static string CreateShipInformations(Mystery mystery)
        {
            string shipInformations = "Schiffe: \r\n\r\n";
            foreach (var oneShip in mystery.ShipList)
            {
                for (int i = 0; i < oneShip.Size; i++)
                {
                    shipInformations += "X";
                }
                shipInformations += "\r\n";
            }
            return shipInformations;
        }

        private static PdfPTable CreateTable(Mystery mystery, bool isForSolution)
        {
            int[] shipColumnCounter = new int[mystery.MysteryCreator.NumberOfColumns];
            int[] shipRowCounter = new int[mystery.MysteryCreator.NumberOfRows];
            

            PdfPTable table = new PdfPTable(mystery.MysteryCreator.NumberOfColumns + 1);
                table.AddCell("X");


            for (int y = 1; y <= mystery.MysteryCreator.NumberOfColumns; y++)
            {
                int countedShips = mystery.FieldList.Where(f => f.IsShipField && f.XCoordinate == y).Count();

                table.AddCell(countedShips.ToString());
            }

            for (int y = 1; y <= mystery.MysteryCreator.NumberOfColumns; y++)
            {
                for (int x = 1; x <= mystery.MysteryCreator.NumberOfRows; x++)
                {
                    if (x == 1)
                    {
                        int countedShips = mystery.FieldList.Where(f => f.IsShipField && f.YCoordinate == y).Count();

                        table.AddCell(countedShips.ToString());
                    }

    
                    Field field = mystery.FieldList.First(f => f.XCoordinate == x && f.YCoordinate == y);
                    PdfPCell cell = new PdfPCell();

                    if (isForSolution)
                    {
                        if (field.IsShipField)
                        {
                            cell.BackgroundColor = BaseColor.ORANGE;
                            cell.Phrase = new Phrase("X");
                        }
                        else
                        {
                            cell.BackgroundColor = BaseColor.BLUE;
                        }
                    }
                    else
                    {
                        switch (field.Status)
                        {
                            case Enum.FieldStatus.Unexplored:
                                cell.BackgroundColor = BaseColor.WHITE;
                                break;
                            case Enum.FieldStatus.Water:
                                cell.BackgroundColor = BaseColor.BLUE;
                                break;
                            case Enum.FieldStatus.Ship:
                                cell.BackgroundColor = BaseColor.ORANGE;
                                cell.Phrase = new Phrase("X");
                                break;
                        }
                    }
                    table.AddCell(cell);

                    if (field.IsShipField)
                    {
                        shipColumnCounter[y - 1]++;
                        shipRowCounter[x - 1]++;
                        var no = field.IsShipField;
                    }
                }
            }
            return table;
        }
    }
}
