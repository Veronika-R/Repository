using System;

namespace Checkers
{
    public class MovePosition : Position
    {
        public Position Start
        {
            get; private set;
        }

        public Position End
        {
            get; private set;
        }

        public MovePosition(Position start, Position end)
        {
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return String.Format("{0}-{1}", Start, End);
        }
    }
}