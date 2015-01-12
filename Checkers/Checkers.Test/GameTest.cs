using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.Test
{
    [TestClass]
    public class GameTest
    {
        private Game game;

        [TestInitialize]
        public void Init()
        {
            game = new Game();
        }

        [TestMethod]
        public void SetPieces_SetupStringOfPieceValues_RightCountOfPieces()
        {
            game.SetPieces("wa1;bb2;bc3;wb4");
            Assert.AreEqual(4, game.Pieces.Count);
        }

        [TestMethod]
        public void SetPieces_MissedPiecePresent_RightCountOfPieces()
        {
            game.SetPieces("wa1;bb2;;wb4");
            Assert.AreEqual(3, game.Pieces.Count);
        }

        [TestMethod]
        public void CheckWalls_InputCorrectPositionOfPeace_PieceOnBoard()
        {
            var isOnBoard = game.CheckWalls(new Position(1, 2));
            Assert.IsTrue(isOnBoard);
        }
    }
}
