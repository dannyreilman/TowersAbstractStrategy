namespace TowersAbstractStrategyLogic
{
    /// <summary>High level immutable configuration class for the board or game as a whole</summary>
    ///
    class Board
    {
        public (int width, int height) Size { get; }
        public Board((int width, int height) size)
        {
            Size = size;
        }
        public override string ToString()
        {
            return $"Board[Size:({Size.width}, {Size.height}]";
        }
    }
}
