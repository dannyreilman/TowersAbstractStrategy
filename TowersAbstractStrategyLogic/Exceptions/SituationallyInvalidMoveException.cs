namespace TowersAbstractStrategyLogic.Exceptions
{
    /// <summary>
    /// Represents a move which is invalid due to the current game state
    /// </summary>
    class SituationallyInvalidMoveException: InvalidMoveException
    {
        public SituationallyInvalidMoveException() : base() { }
        public SituationallyInvalidMoveException(string message) : base(message) { }
    }
}
