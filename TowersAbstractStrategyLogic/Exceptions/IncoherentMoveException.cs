namespace TowersAbstractStrategyLogic.Exceptions
{
    /// <summary>
    /// Represents a move which is outside the bounds of the board and could not be valid regardless of game state.
    /// </summary>
    class IncoherentMoveException : InvalidMoveException
    {
        public IncoherentMoveException() : base() { }
        public IncoherentMoveException(string message) : base(message) { }
    }
}
