using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Checkers
{
    public class Game
    {
        public const int BoardSize = 8;

        public List<IPiece> Pieces { get; private set; }

        public Color Turn { get; private set; }

        public Game():this (Color.White)
        {
        }

        public Game(Color turn)
        {
            Pieces = new List<IPiece>();
            Turn = turn;
        }

        public void Initialize(string inputPieces, string turn)
        {
            SetTurn(turn);
            SetPieces(inputPieces);
        }

        public void SetPieces(string inputPieces)
        {
            var pieces = inputPieces.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)
                .Select(Piece.ParsePiece);

            Pieces.AddRange(pieces);
        }

        public Color SetTurn(string turn)
        {
            switch (turn.ToUpper())
            {
                case "W":
                    return Color.White;
                case "B":
                    return Color.Black;
                default:
                    throw new CheckersException("Turn has not been recognized, ex. W or w, B or b");
            }
        }

        public bool CheckWalls(Position position)
        {
            if ((position.X < 0) || (position.Y < 0) || (position.X > BoardSize - 1) || (position.Y > BoardSize - 1))
            {
                return false;
            }
            return true;
        }

        public IPiece PieceAt(Position position)
        {
            return Pieces.FirstOrDefault(p => p.Position.X == position.X && p.Position.Y == position.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetPosition"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool IsCaptured(Position targetPosition, Color color)
        {
            return PieceAt(targetPosition) != null && PieceAt(targetPosition).Color != color;
        }

        public static IEnumerable<Position> GetAllMoves(Game game)
        {
            var moves = new List<Position>();
            var pieces = game.Pieces.Where(p => p.Color == game.Turn).ToList();
            
            foreach (var piece in pieces)
            {
                moves.AddRange(piece.GetJumpsRecursive(game, piece.Position, null));
            }
            
            if (moves.Count == 0)
            {
                foreach (var piece in pieces)
                {
                    moves.AddRange(piece.Moves(game));
                }
            }

            return moves;
        }

    }
}