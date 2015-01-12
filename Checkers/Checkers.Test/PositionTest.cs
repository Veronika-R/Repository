using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.Test
{
    [TestClass]
    public class PositionTest
    {

        [TestMethod]
        [ExpectedException(typeof(CheckersException), "Address 'qwe' doesn't match address criteria, ex. a1 or b8")]
        public void GetPositionCoordinates_PositionHasInvalidAddress_ExceptionShouldBeThrown()
        {
            Position.GetPositionByAdress("qwe");
        }

        [TestMethod]
        [ExpectedException(typeof(CheckersException), "Address 'L1' doesn't match address criteria : 1st coordinate")]
        public void GetPositionCoordinates_PositionHasInvalidAddressFirstCoordinate_ExceptionShouldBeThrown()
        {
            Position.GetPositionByAdress("L1");
        }

        [TestMethod]
        [ExpectedException(typeof(CheckersException), "Address 'DB' doesn't match address criteria : 2nd coordinate")]
        public void GetPositionCoordinates_PositionHasInvalidAddressSecondCoordinate_ExceptionShouldBeThrown()
        {
            Position.GetPositionByAdress("DB");
        }

        [TestMethod]
        public void GetPositionCoordinates_PositionHasValidAddress_PositionHasCreated()
        {
            var position = Position.GetPositionByAdress("f6");

            Assert.IsTrue(position.X == 5 && position.Y==5);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckersException), "Address doesn't match address criteria: 'B7' is not a dark Position")]
        public void GetPositionCoordinates_PositionHasInvalidAddressWhitePosition_ExceptionShouldBeThrown()
        {
            Position.GetPositionByAdress("B7");
        }

        [TestMethod]
        public void ToString_PositionHasInitialCoordinates()
        {
            var position = new Position(0, 0);

            Assert.AreEqual("A1", position.ToString());
        }

        [TestMethod]
        public void ToString_SetUpPosition_AddressReturned()
        {
            var position = new Position(6, 6);

            Assert.AreEqual("G7", position.ToString());
        }

        [TestMethod]
        public void ToString_SetUpSecuenceOfPosition_SecuenceofAddressesReturned()
        {
            var positions = new[] {new Position(6, 0), new Position(4, 2), new Position(6, 4)};
            var secuence = new SequentialPosition(positions, new Position(5, 1));
            
            Assert.AreEqual("G1-E3-G5", secuence.ToString());

        }
    }
}