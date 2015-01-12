using System.Collections.Generic;
using System.Linq;

namespace Checkers.Model
{
    public class Piece:IPiece
    {
        public Piece(Color color,Position position, bool isKing = false)
        {
            Color = color;
            IsKing = isKing;
            Position = position;
        }

        public Color Color
        {
            get;
            private set;
        }

        public bool IsKing
        {
            get;
            private set;
        }

        public Position Position
        {
            get;
            private set;
        }


        public IEnumerable<MovePosition> Moves(Game game)
        {
            // Get possible direction for this piece
            var moves = GetMovingDirection(Position.X, Position.Y);
            
            // Only allow valid moves
            var validMoves = new List<MovePosition>();
            foreach (var move in moves)
            {
                if (game.CheckWalls(move) && (game.PieceAt(move) == null))
                {
                    validMoves.Add(new MovePosition(Position, move));
                }
            }

            return validMoves;
        }

        public List<SequentialPosition> GetJumpsRecursive(Game game, Position position, SequentialPosition originalMove)
        {
            var jumps = new List<SequentialPosition>();
            var directions = GetJumpDirection(position.X, position.Y);

            foreach (var targetPosition in directions)
            {
                TryJump(game, position, originalMove, targetPosition, jumps);
            }

            if (jumps.Any())
            {
                return jumps;
            }

            return originalMove != null
                   ? new List<SequentialPosition> {originalMove}
                   : new List<SequentialPosition>();
        }

        private void TryJump(Game game, Position position, SequentialPosition oriMove, Position targetPos, List<SequentialPosition> jumps)
        {
            //check that current piece on the Board
            while (game.CheckWalls(targetPos))
            {
                if (oriMove != null && oriMove.Captured.Any(p => p.X == targetPos.X && p.Y == targetPos.Y))
                {
                    break;
                }
                //check target position if it can be captured
                if (game.IsCaptured(targetPos, Color))
                {
                    Jump(game, position, oriMove, targetPos, jumps);
                    if (!IsKing)
                    {
                        break;
                    }
                }
                if (IsKing)
                {
                    var newTargetPos = PotentialJump(position, targetPos);
                    position = targetPos;
                    targetPos = newTargetPos;
                }
                else
                {
                    break;
                }
            }
        }

        private void Jump(Game game, Position position, SequentialPosition originMove, Position targetPos, List<SequentialPosition> jumps)
        {
            // Look for a valid space *beyond* this piece
            var jumpPosition = PotentialJump(position, targetPos);

            if (game.CheckWalls(jumpPosition) && game.PieceAt(jumpPosition) == null)
            {
                var newMove = AddPositionToSecuence(originMove, targetPos, jumpPosition);
                
                if (newMove != null)
                {
                    if (!IsKing)
                    {
                        IsKing = Crowned(jumpPosition);
                    }
                    var moves2Add = GetJumpsRecursive(game, jumpPosition, newMove);
                    if (moves2Add != null)
                        jumps.AddRange(moves2Add);
                }
            }
        }

        private SequentialPosition AddPositionToSecuence(SequentialPosition originMove, Position targetPos, Position jumpPosition)
        {
            SequentialPosition newMove = null;
            if (originMove != null)
            {
                if (!originMove.Captured.Any(p => p.X == targetPos.X && p.Y == targetPos.Y))
                {
                    newMove = new SequentialPosition(originMove);
                    newMove.MoveSequance.Add(jumpPosition);
                    newMove.Captured.Add(targetPos);
                }
            }
            else
            {
                newMove = new SequentialPosition(new List<Position> {Position, jumpPosition}, targetPos);
            }
            return newMove;
        }

        private IList<Position> GetJumpDirection(int x, int y)
        {
            var moves = new List<Position>();
            moves.AddRange(MoveToBlackSide(x, y));
            moves.AddRange(MoveToWhiteSide(x, y));
            return moves;
        }

        private static Position PotentialJump(Position position, Position targetPos)
        {
            return new Position(targetPos.X * 2 - position.X, targetPos.Y * 2 - position.Y);
        }

        private IEnumerable<Position> GetMovingDirection(int x, int y)
        {
            var moves = new List<Position>();
            if (IsKing || Color == Color.White)
            {
                moves.AddRange(MoveToBlackSide(x, y));
            }
            if (IsKing || Color == Color.Black)
            {
                moves.AddRange(MoveToWhiteSide(x, y));
            }
            return moves;
        }

        private IEnumerable<Position> MoveToWhiteSide(int x, int y )
        {
            return new []
            {
                new Position(x - 1, y + 1), 
                new Position(x + 1, y + 1)
            };
        }

        private IEnumerable<Position>  MoveToBlackSide(int x, int y)
        {
            return new []
            {
                new Position(x - 1, y - 1), 
                new Position(x + 1, y - 1)
            };
        }
        private bool Crowned(Position position)
        {
            if (Color == Color.White)
            {
                if (position.Y == Game.BoardSize - 1)
                {
                    return true;
                }
            }
            else
            {
                if (position.Y == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static Piece ParsePiece(string input)
        {
            PieceDesciptionHasValue(input);
            if (input.Length != 3)
                throw new CheckersException("Piece has incorrect description: length should be 3, ex 'wa1'");

            var color = GetPieceColor(input);
            var isQueen = Crowned(input);
            var position = Position.GetPositionByAdress(input.Substring(1, 2).ToUpper());
            return new Piece(color, position, isQueen);

        }

        private static Color GetPieceColor(string strColor)
        {
            PieceDesciptionHasValue(strColor);

            switch (strColor.Substring(0, 1).ToUpper())
            {
                case "W":
                    return Color.White;
                case "B":
                    return Color.Black;
                default:
                    throw new CheckersException("Piece has incorrect description: color should be 'W' or 'B'");
            }
        }

        private static bool Crowned(string strIsKing)
        {
            PieceDesciptionHasValue(strIsKing);

            switch (strIsKing.Substring(0, 1))
            {
                case "W":
                case "B":
                    return true;
                case "w":
                case "b":
                    return false;
                default:
                    throw new CheckersException("Piece has incorrect description: first letter should be 'W' or 'B' or 'w' or 'b'");
            }
        }

        private static void PieceDesciptionHasValue(string input)
        {
            if (input == null)
                throw new CheckersException("Piece has incorrect description.");
        }
    }
}