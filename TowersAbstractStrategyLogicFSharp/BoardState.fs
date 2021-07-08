namespace GameLogic

module BoardState =
    open DomainTypes

    let createStateFromBoard board =
        let createStateFromTuple tuple = 
            Array2D.create <|| tuple <| SquareState 0

        Dimension.applyInner createStateFromTuple board.BoardShape

    let newGame board = 
        {WhoseTurn = Player White; State = createStateFromBoard board; MaxHeight = Length.value board.MaxHeight}

    let isGameOver boardState = boardState.WhoseTurn = GameOver

    let isMyTurn player boardState = boardState.WhoseTurn = Player player
    
    let getHeight (squareState:SquareState):int =
        let (SquareState state) = squareState
        state
        
    let tryApplyByCoordinate (boardState:BoardState) f (x,y) failure =
        match (x,y) with
        |(x,_) when x < 0 || x > Array2D.base1 boardState.State -> failure
        |(_,y) when y < 0 || y > Array2D.base2 boardState.State -> failure
        |(_,_) -> let (SquareState s) = boardState.State.[x,y]
                  f s