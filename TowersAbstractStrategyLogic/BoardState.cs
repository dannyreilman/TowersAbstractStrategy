using TowersAbstractStrategyLogic.Exceptions;

namespace TowersAbstractStrategyLogic.Exceptions
{
    /// <summary>Immutable class representing the board state at a given moment</summary>
    ///
    class BoardState
    {
        //Dependencies
        Board board;

        public BoardState(Board board)
        {
            this.board = board;
        }
        
        public BoardState(BoardState state, HalfMove move)
        {
            if (move.Coordinates.x > board.BoardWidth || move.Coordinates.y > board.BoardHeight)
                throw new IncoherentMoveException($"Move {move.ToString()} is outside board {ToString()}");
        }

        public BoardState ApplyMove(HalfMove move)
        {
            return new BoardState(this, move);
        }
    }
}
