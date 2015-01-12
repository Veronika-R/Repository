using Checkers.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.Test
{
    [TestClass]
    public class PieceParserTest
    {
        [TestMethod]
        [ExpectedException(typeof(CheckersException), "Piece has incorrect description.")]
        public void ParsePiece_InputIncorrectPieceDataNullable_ExceptionShouldBeThrown()
        {
            Piece.ParsePiece(null);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckersException), "Piece has incorrect description: length should be 3, ex 'wa1'")]
        public void ParsePiece_InputIncorrectPieceDataLessSymbols_ExceptionShouldBeThrown()
        {
            Piece.ParsePiece("");
        }

        [TestMethod]
        [ExpectedException(typeof(CheckersException), "Piece has incorrect description: length should be 3, ex 'wa1'")]
        public void ParsePiece_InputIncorrectPieceDataMoreSymbold_ExceptionShouldBeThrown()
        {
            Piece.ParsePiece("wess");
        }

        [TestMethod]
        public void ParsePiece_InputIncorrectPieceDataMoreSymbold_PieceShouldBeReturn()
        {
            var piece = Piece.ParsePiece("Wa1");

            Assert.AreEqual(Color.White, piece.Color);
            Assert.AreEqual(true, piece.IsKing);
            Assert.AreEqual("A1", piece.Position.ToString());

        }

        [TestMethod]
        public void ParsePiece_InputCorrectPieceDataWhiteColor_ColorShouldBeReturned()
        {
            var piece = Piece.ParsePiece("Wa1");
            Assert.AreEqual(Color.White, piece.Color);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckersException), "Piece has incorrect description.")]
        public void ParsePiece_InputIncorrectPieceData_ExceptionShouldBeThrown()
        {
            Piece.ParsePiece("da1");
        }

        [TestMethod]
        public void ParsePiece_InputPieceDataBlackColor_BlackColor()
        {
            var piece = Piece.ParsePiece("Ba1");

            Assert.AreEqual(Color.Black, piece.Color);
        }


        [TestMethod]
        public void ParsePiece_InputCorrectPieceDataPieceIsKing_King()
        {
            var piece = Piece.ParsePiece("Wa1");

            Assert.IsTrue(piece.IsKing);
        }

        [TestMethod]
        public void ParsePiece_InputCorrectPieceDataPieceIsNotKing_NotKing()
        {
            var piece = Piece.ParsePiece("ba1");

            Assert.IsFalse(piece.IsKing);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckersException), "Piece has incorrect description.")]
        public void ParsePiece_InputCorrectPieceDataNullable_ExceptionShouldBeThrown()
        {
            Piece.ParsePiece(null);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckersException), "Piece has incorrect description: first letter should be 'W' or 'B' or 'w' or 'b'")]
        public void ParsePiece_InputIncorrectPieceDataIsKing_ExceptionShouldBeThrown()
        {
            Piece.ParsePiece("Va1");
        }
    }
}