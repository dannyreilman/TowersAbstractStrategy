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
