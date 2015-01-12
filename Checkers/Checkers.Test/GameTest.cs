using System.Collections.Generic;
using Checkers.Model;
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
        public void Initialize_SetupStringOfPieceValues_RightCountOfPieces()
        {
            game.Initialize("wa1;bb2;bc3;wb4","W");
            Assert.AreEqual(4, game.Pieces.Count);
        }

        [TestMethod]
        public void Initialize_MissedPiecePresent_RightCountOfPieces()
        {
            game.Initialize("wa1;bb2;;wb4","W");
            Assert.AreEqual(3, game.Pieces.Count);
        }

        [TestMethod]
        public void Initialize_SetTurnForWhitePieces_TurnEqualsWhite()
        {
            game.Initialize("wa1;bb2;;wb4","W");
            Assert.AreEqual(Color.White, game.Turn);
        }
        
        [TestMethod]
        [ExpectedException(typeof(CheckersException),"Turn has not been recognized, ex. W or w, B or b")]
        public void Initialize_SetTurnForBlackPieces_ExceptionExpected()
        {
            game.Initialize("wa1;bb2;;wb4","A");
        }

        [TestMethod]
        public void CheckWalls_InputCorrectPositionOfPeace_PieceOnBoard()
        {
            var isOnBoard = game.CheckWalls(new Position(1, 2));
            Assert.IsTrue(isOnBoard);
        }
        
        [TestMethod]
        public void PieceAt_InputCorrectPositionOfPeace_PieceOnBoard()
        {
            game.Pieces.AddRange(new List<IPiece>
            {
                new Piece(Color.Black, new Position(1,2))
            });
            var piece = game.PieceAt(new Position(1, 2));

            Assert.AreEqual(1,piece.Position.X);
            Assert.AreEqual(2,piece.Position.Y);
        }
        
        [TestMethod]
        public void IsCaptured_InputCorrectPositionOfPeace_PieceCaptured()
        {
            game.Pieces.AddRange(new List<IPiece>
            {
                new Piece(Color.Black, new Position(1,2))
            });

            var isCaptured = game.IsCaptured(new Position(1, 2), Color.White);
            Assert.IsTrue(isCaptured);
        }
    }
}
