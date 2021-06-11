using System;

namespace TowersAbstractStrategyLogic.Exceptions
{
    /// <summary>
    /// Represents a move that is illegal for some reason
    /// </summary>
    class InvalidMoveException : Exception
    {
        public InvalidMoveException() : base() { }
        public InvalidMoveException(string message) : base(message) { }
    }
}
