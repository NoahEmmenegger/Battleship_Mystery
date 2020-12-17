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
        /// <summary>
        /// Erstellt ein PDF und schreibt das Rästel in das PDF
        /// </summary>
        /// <param name="mystery">Das Räsel, welches exportiert werden soll</param>
        /// <param name="filePath">Der Pfad von der PDF Datei</param>
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
        /// <summary>
        /// Erstellt ein PDF mit der Lösung vom Rätsel
        /// </summary>
        /// <param name="mystery">Rätsel von welchem die Lösung exportiert werden soll</param>
        /// <param name="filePath">Der Pfad von der PDF Datei</param>
        public static void ExportSolution(Mystery mystery, string filePath)
        {
            Document doc = new Document();
            PdfPTable table = CreateTable(mystery, true);
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));

            doc.Open();
            doc.Add(table);
            doc.Close();
        }
        /// <summary>
        /// Erstellt die Informationen, von welchem Schiffstyp, wieviele Schiffe existieren
        /// </summary>
        /// <param name="mystery">Das Mystery, welches für die Generation verwendet werden soll</param>
        /// <returns></returns>
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
        /// <summary>
        /// Erstellt die Tabelle für den PDF Export
        /// </summary>
        /// <param name="mystery">Das Mystery welches für die Generation verwendet werden soll</param>
        /// <param name="isForSolution">Definiert, ob die Tabelle für die Lösung oder für das Rätsel generiert werden soll.</param>
        /// <returns></returns>
        private static PdfPTable CreateTable(Mystery mystery, bool isForSolution)
        {
            int[] shipColumnCounter = new int[mystery.MysteryCreator.NumberOfColumns];
            int[] shipRowCounter = new int[mystery.MysteryCreator.NumberOfRows];
            

            PdfPTable table = new PdfPTable(mystery.MysteryCreator.NumberOfColumns + 1);
                table.AddCell("");


            for (int y = 1; y <= mystery.MysteryCreator.NumberOfColumns; y++)
            {
                int countedShips = mystery.FieldList.Where(f => f.IsShipField && f.XCoordinate == y).Count();

                table.AddCell(countedShips.ToString());
            }

            for (int y = 1; y <= mystery.MysteryCreator.NumberOfRows; y++)
            {
                for (int x = 1; x <= mystery.MysteryCreator.NumberOfColumns; x++)
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
                        shipRowCounter[y - 1]++;
                        shipColumnCounter[x - 1]++;
                        var no = field.IsShipField;
                    }
                }
            }
            return table;
        }
    }
}
