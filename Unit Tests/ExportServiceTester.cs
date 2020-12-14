using Battleship_Mystery.Business;
using Battleship_Mystery.Business.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Unit_Tests
{
    [TestClass]
    public class ExportServiceTester
    {
        [TestMethod]
        public void ExportTest()
        {
            string tmpFilename = Path.GetTempFileName();

            MysteryCreator mysteryCreator = new MysteryCreator();
            mysteryCreator.NumberOfColumns = 7;
            mysteryCreator.NumberOfRows = 7;
            mysteryCreator.NumberOfShips = 12;
            Mystery mystery = mysteryCreator.Create();
            ExportService.Export(mystery, tmpFilename);

            if (File.Exists(tmpFilename))
            {
                Assert.IsTrue(true);
                File.Delete(tmpFilename);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void ExportSolutionTest()
        {
            string tmpFilename = Path.GetTempFileName();

            MysteryCreator mysteryCreator = new MysteryCreator();
            mysteryCreator.NumberOfColumns = 7;
            mysteryCreator.NumberOfRows = 7;
            mysteryCreator.NumberOfShips = 12;
            Mystery mystery = mysteryCreator.Create();
            ExportService.ExportSolution(mystery, tmpFilename);

            if (File.Exists(tmpFilename))
            {
                Assert.IsTrue(true);
                File.Delete(tmpFilename);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }
    }
}
