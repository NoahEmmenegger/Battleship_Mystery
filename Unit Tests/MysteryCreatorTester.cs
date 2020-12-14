using Battleship_Mystery.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Unit_Tests
{
    [TestClass]
    public class MysteryCreatorTester
    {
        [TestMethod]
        public void CreateTest()
        {
            MysteryCreator mysteryCreator = new MysteryCreator();
            mysteryCreator.NumberOfColumns = 7;
            mysteryCreator.NumberOfRows = 7;
            mysteryCreator.NumberOfShips = 12;
            Mystery mystery = mysteryCreator.Create();
            Assert.IsTrue(mystery != null);
            Assert.IsTrue(mystery.FieldList.Count == 49 && mystery.FieldList.Where(f => f.IsShipField == true).ToList().Count == 12);
        }

        [TestMethod]
        public void ValidationTest()
        {
            MysteryCreator mysteryCreator1 = new MysteryCreator();
            mysteryCreator1.NumberOfColumns = 7;
            mysteryCreator1.NumberOfRows = 7;
            mysteryCreator1.NumberOfShips = 12;
            mysteryCreator1.NumberOfShips = 13;
            Assert.IsTrue(mysteryCreator1.NumberOfShips == 12);

            MysteryCreator mysteryCreator2 = new MysteryCreator();
            mysteryCreator2.NumberOfColumns = 10;
            mysteryCreator2.NumberOfRows = 10;
            mysteryCreator2.NumberOfShips = 20;
            mysteryCreator2.NumberOfColumns = 7;
            Assert.IsTrue(mysteryCreator2.NumberOfShips == 17);

            MysteryCreator mysteryCreator3 = new MysteryCreator();
            mysteryCreator3.NumberOfColumns = 10;
            mysteryCreator3.NumberOfRows = 10;
            mysteryCreator3.NumberOfShips = 20;
            mysteryCreator3.NumberOfRows = 7;
            Assert.IsTrue(mysteryCreator3.NumberOfShips == 17);
        }
    }
}
