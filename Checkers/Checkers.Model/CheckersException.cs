using System;

namespace Checkers.Model
{
    public class CheckersException : Exception
    {
        public CheckersException(string message) : base(message)
        {
        }
    }
}