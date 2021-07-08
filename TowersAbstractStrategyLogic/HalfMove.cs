using System;

namespace TowersAbstractStrategyLogic
{
    /// <summary>A half move is one player's move</summary>
    ///
    class HalfMove
    {
        public (int x, int y) Coordinates { get; }

        public HalfMove((int x, int y) coordinates)
        {
            if (coordinates.x < 0 || coordinates.y < 0)
                throw new ArgumentOutOfRangeException("Coordinates must be non-negative");
            Coordinates = coordinates;
        }
    }
}
