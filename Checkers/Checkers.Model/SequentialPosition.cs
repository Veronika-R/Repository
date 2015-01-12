using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Model
{
    public class SequentialPosition : Position
    {
        public IList<Position> MoveSequance;

        public IList<Position> Captured;

        public SequentialPosition(IList<Position> sequence, Position capturedPiece)
        {
            MoveSequance = sequence;
            Captured = new List<Position> { capturedPiece };
        }

        public SequentialPosition(SequentialPosition originalMove)
        {
            MoveSequance = originalMove.MoveSequance.ToList();
            Captured = originalMove.Captured.ToList(); ;
        }

        public override string ToString()
        {
            var str = new StringBuilder();
            foreach (var position in MoveSequance)
            {
                str.AppendFormat("{0}-", position);
            }
            str.Remove(str.Length-1,1);
            return str.ToString();
        }
    }
}