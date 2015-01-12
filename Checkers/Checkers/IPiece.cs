using System.Collections.Generic;

namespace Checkers
{
    public interface IPiece
    {
        Color Color { get;}
        bool IsKing { get;}
        Position Position { get;}

        IEnumerable<MovePosition> Moves(Game game);
        List<SequentialPosition> GetJumpsRecursive(Game game, Position position, SequentialPosition originalMove);
    }
}