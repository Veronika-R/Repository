using System;

namespace Checkers
{
    public class CheckersException : Exception
    {
        public CheckersException(string message) : base(message)
        {
        }
    }
}