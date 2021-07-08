namespace GameLogic

module DomainTypes =

    type Board = 
        {
        BoardShape: Dimension.T
        MaxHeight: Length.T
        }
    
    type SquareState = SquareState of int
    type Player = White | Black
    type WhoseTurn = Player of Player | GameOver
    type BoardState = 
        {
        WhoseTurn: WhoseTurn
        State: SquareState[,]
        MaxHeight: int
        }

    type Move = Move of Coordinate.T