using System;

namespace Checkers.Model
{
    public class Position
    {
        private const int OriginCoordinateX = 'A';
        private const int OriginCoordinateY = 1;

        public int X { get; set;}

        public int Y{ get; set; }

        protected Position()
        {
        }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Position GetPositionByAdress(string address)
        {
            if (address == null || address.Length != 2)
            {
                throw new CheckersException(String.Format("Address '{0}' doesn't match address criteria, ex. a1 or b8", address));
            }
            string addressUpper = address.ToUpper();
  
            int x = addressUpper[0] - OriginCoordinateX;
            int y = addressUpper[1] - Char.Parse(OriginCoordinateY.ToString());

            if (x < 0 || x >= Game.BoardSize)
            {
                throw new CheckersException(String.Format("Address '{0}' doesn't match address criteria: 1nd coordinate", address));
            }
            if (y < 0 || y >= Game.BoardSize)
            {
                throw new CheckersException(String.Format("Address '{0}' doesn't match address criteria: 2st coordinate",address));
            }
            if ((x + y)%2 != 0)
            {
                throw new CheckersException(String.Format("Address doesn't match address criteria: '{0}' is not a dark Position", address));
            }
            return new Position(x, y);
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", (char)(OriginCoordinateX + X), OriginCoordinateY + Y);
        }
    }
}