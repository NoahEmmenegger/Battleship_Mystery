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
    public class SaveLoadServiceTester
    {
        [TestMethod]
        public void SaveTest()
        {
            string tmpFilename = Path.GetTempFileName();

            MysteryCreator mysteryCreator = new MysteryCreator();
            mysteryCreator.NumberOfColumns = 7;
            mysteryCreator.NumberOfRows = 7;
            mysteryCreator.NumberOfShips = 12;
            Mystery mystery = mysteryCreator.Create();
            SaveLoadService.Save(mystery, tmpFilename);

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
        public void LoadTest()
        {
            string tmpFilename = Path.GetTempFileName();

            MysteryCreator mysteryCreator1 = new MysteryCreator();
            mysteryCreator1.NumberOfColumns = 7;
            mysteryCreator1.NumberOfRows = 7;
            mysteryCreator1.NumberOfShips = 12;
            Mystery mystery1 = mysteryCreator1.Create();
            SaveLoadService.Save(mystery1, tmpFilename);

            MysteryCreator mysteryCreator2 = new MysteryCreator();
            mysteryCreator2.NumberOfColumns = 2;
            mysteryCreator2.NumberOfRows = 2;
            mysteryCreator2.NumberOfShips = 1;
            Mystery mystery2 = mysteryCreator2.Create();
            mystery2 = SaveLoadService.Load(mystery2, tmpFilename);
            Assert.IsTrue(mystery2.FieldList.Count == mystery1.FieldList.Count && mystery1.ShipList.Count == mystery2.ShipList.Count);
            if (File.Exists(tmpFilename))
            {
                File.Delete(tmpFilename);
            }         
        }
    }
}
