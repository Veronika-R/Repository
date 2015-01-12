using System.Linq;
using Checkers.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.Test
{
    [TestClass]
    public class PieceMovesTest
    {

        private Game game;

        [TestInitialize]
        public void Init()
        {
            game = new Game();
        }

        [TestMethod]
        public void Move_PieceWhiteNonCrowned_OnlyTwoMovesAvaliable()
        {
            var piece = new Piece(Color.White, new Position(2, 3));
            var moves = piece.Moves(game).ToList();

            Assert.AreEqual(2, moves.Count);
            Assert.AreEqual("C4-B3", moves[0].ToString());
            Assert.AreEqual("C4-D3", moves[1].ToString());
        }

        [TestMethod]
        public void Move_PieceBlackNonCrowned_OnlyTwoMovesAvaliable()
        {
            var piece = new Piece(Color.Black, new Position(2, 3));
            var moves = piece.Moves(game).ToList();

            Assert.AreEqual(2, moves.Count);
            Assert.AreEqual("C4-B5", moves[0].ToString());
            Assert.AreEqual("C4-D5", moves[1].ToString());
        
        }

        [TestMethod]
        public void Move_PiecesPlacedPieceShouldMove_TopRight()
        {
            var piece = new Piece(Color.White, new Position(5, 2));
            game.Pieces.AddRange(new[]
            {
                new Piece(Color.Black, new Position(4, 1)),
                piece
            });
            var moves = piece.Moves(game).ToList();

            Assert.AreEqual(1, moves.Count);
            Assert.AreEqual("F3-G2", moves[0].ToString());
        }

        [TestMethod]
        public void Move_PieceCrowned_AllDirections()
        {
            var piece = new Piece(Color.White, new Position(2, 3), true);
            var moves = piece.Moves(game).ToList();

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual("C4-B3", moves[0].ToString());
            Assert.AreEqual("C4-D3", moves[1].ToString());
            Assert.AreEqual("C4-B5", moves[2].ToString());
            Assert.AreEqual("C4-D5", moves[3].ToString());
        }

        [TestMethod]
        public void Move_PiecesPlacedPieceShouldMove_BottomLeft()
        {
            var piece = new Piece(Color.White, new Position(0, 7), true);
            var moves = piece.Moves(game).ToList();

            Assert.AreEqual(1, moves.Count);
            Assert.AreEqual("A8-B7", moves[0].ToString());
        }

        [TestMethod]
        public void Move_PiecesPlacedKingShouldMove_TopRight()
        {
            var piece = new Piece(Color.White, new Position(0, 7), true);
            var moves = piece.Moves(game).ToList();

            Assert.AreEqual(1, moves.Count);
            Assert.AreEqual("A8-B7", moves[0].ToString());
        }

        [TestMethod]
        public void Jump_PiecesPlacedPieceShouldJump_AllDirections()
        {
            var piece = new Piece(Color.White, new Position(2, 3));

            game.Pieces.AddRange(new[]
            {
                new Piece(Color.Black, new Position(1, 2)),
                new Piece(Color.Black, new Position(3, 2)),
                new Piece(Color.Black, new Position(1, 4)),
                piece
            });
            var moves = piece.GetJumpsRecursive(game, piece.Position,null).ToList().ToList();

            Assert.AreEqual(3, moves.Count, "Should only have 3 moves");
            Assert.AreEqual("C4-A2", moves[0].ToString());
            Assert.AreEqual("C4-E2", moves[1].ToString());
            Assert.AreEqual("C4-A6", moves[2].ToString());
        }

        [TestMethod]
        public void Jump_PiecesPlacedKingShouldJump_AllDirections()
        {
            var piece = new Piece(Color.White, new Position(2, 3), true);

            game.Pieces.AddRange(new[]
            {
                new Piece(Color.Black, new Position(1, 2)),
                new Piece(Color.Black, new Position(3, 2)),
                new Piece(Color.Black, new Position(1, 4)),
                piece
            });
            var moves = piece.GetJumpsRecursive(game, piece.Position, null).ToList();
            
            Assert.AreEqual(3, moves.Count);
            Assert.AreEqual("C4-A2", moves[0].ToString());
            Assert.AreEqual("C4-E2", moves[1].ToString());
            Assert.AreEqual("C4-A6", moves[2].ToString());
         
        }

        [TestMethod]
        public void Jump_PiecesPlacedKingShouldJump_NoJumpsAvaliable()
        {
            var piece = new Piece(Color.White, new Position(1, 6), true);
            game.Pieces.AddRange(new[]
            {
                new Piece(Color.Black, new Position(0, 7)),
                piece
            });
            var moves = piece.GetJumpsRecursive(game, piece.Position, null).ToList();
            
            Assert.AreEqual(0, moves.Count);
        }

        [TestMethod]
        public void Jump_OnlyBlackPieceCanBeCaptured_AllBlackPiecesCaptured()
        {
            var piece = new Piece(Color.White, new Position(2, 3), true);
            game.Pieces.AddRange(new[]
            {
                new Piece(Color.Black, new Position(1, 2)),
                new Piece(Color.White, new Position(3, 2)),
                new Piece(Color.Black, new Position(1, 4)),
                piece
            });
            var moves = piece.GetJumpsRecursive(game, piece.Position, null).ToList();
            
            Assert.AreEqual(2, moves.Count);
            Assert.AreEqual("C4-A2", moves[0].ToString());
            Assert.AreEqual("C4-A6", moves[1].ToString());
        }

        [TestMethod]
        public void Jump_PiecesPlacedPieceShouldJump_OnlyOneJumpAvaliable()
        {
            var piece = new Piece(Color.White, new Position(7, 2));

            game.Pieces.AddRange(new[]
            {
                new Piece(Color.Black, new Position(1, 4)),
                new Piece(Color.Black, new Position(1, 2)),
                new Piece(Color.Black, new Position(6, 1)),
                new Piece(Color.Black, new Position(5, 2)),
                piece
            });
            var moves = piece.GetJumpsRecursive(game, piece.Position, null).ToList();

            Assert.AreEqual(1, moves.Count, "Should only have 1 moves");
            Assert.AreEqual("H3-F1", moves[0].ToString());
        }

        [TestMethod]
        public void Jump_PiecesPlacedPieceShouldJump_NoJumpsAvaliable()
        {
            var piece = new Piece(Color.White, new Position(3, 4));
            game.Pieces.AddRange(new[]
            {
                new Piece(Color.Black, new Position(1, 2)),
                new Piece(Color.Black, new Position(3, 2)),
                new Piece(Color.Black, new Position(1, 4)),
                new Piece(Color.Black, new Position(5, 2)),
                piece
            });
            var moves = piece.GetJumpsRecursive(game, piece.Position, null).ToList();

            Assert.AreEqual(0, moves.Count, "Should have no moves");
        }

        [TestMethod]
        public void Jump_PiecesPlacedPieceShouldJump_White1()
        {

            var piece = new Piece(Color.White, new Position(4, 5));
            game.Pieces.AddRange(new[]
            {
                new Piece(Color.Black, new Position(1, 6)),
                new Piece(Color.Black, new Position(1, 4)),
                new Piece(Color.Black, new Position(3, 6)),
                new Piece(Color.Black, new Position(3, 4)),
                piece
            });

            var moves = piece.GetJumpsRecursive(game, piece.Position, null).ToList();

            Assert.AreEqual(2, moves.Count, "Should only have 2 moves");
            Assert.AreEqual("E6-C4-A6-C8", moves[0].ToString());
            Assert.AreEqual("E6-C8-A6-C4", moves[1].ToString());
        }

        [TestMethod]
        public void Jump_PiecesPlacedKingShouldJump_OnlyOneSecuantialJump()
        {
             var piece = new Piece(Color.White, new Position(7, 4),true);
            game.Pieces.AddRange(new[]
            {
                new Piece(Color.Black, new Position(1, 6)),
                new Piece(Color.Black, new Position(1, 4)),
                new Piece(Color.Black, new Position(3, 6)),
                new Piece(Color.Black, new Position(3, 4)),
                new Piece(Color.Black, new Position(5, 2)),
                new Piece(Color.Black, new Position(6, 1)),
                new Piece(Color.White, new Position(4, 5)),
                piece
            });
            var moves = piece.GetJumpsRecursive(game, piece.Position, null).ToList();

            Assert.AreEqual(1, moves.Count, "Should only have 1 moves");
            Assert.AreEqual("H5-E2-A6-C8", moves[0].ToString());
        }
    }
}