namespace GameLogic

module Move =
    open DomainTypes

    let isOutOfBounds boardState move = Coordinate.applyInner(fun (x,y) -> Array2D.base1 boardState.State > x && Array2D.base2 boardState.State > y) move 
    
    let getHeight (squareState:SquareState):int =
        let (SquareState state) = squareState
        state
        
    /// <summary>Checks if the move is valid based on height rules.
    /// A move cannot be played higher than the board's max height (A piece is height one when initially placed and each subsequent placement on the same tile increases height by one).</summary>
    ///<returns>True if invalid, false is valid.</returns>
    let isTooTall boardState move = 
        match boardState.WhoseTurn with
        |Player White -> Coordinate.applyInner(fun (x,y) -> getHeight boardState.State.[x,y] >= boardState.MaxHeight) move
        |Player Black -> Coordinate.applyInner(fun (x,y) -> getHeight boardState.State.[x,y] <= -1 * boardState.MaxHeight) move
        |GameOver     -> false
        
    let tryFind (boardState:BoardState) (x,y) predicate =
        match (x,y) with
        |(x,_) when x < 0 || x > Array2D.base1 boardState.State -> false
        |(_,y) when y < 0 || y > Array2D.base2 boardState.State -> false
        |(_,_) -> predicate boardState.State.[x,y]

    /// <summary>Checks if the move is valid based on diagonal rules.
    /// A move cannot be played at a diagonal distance from an opponents piece less than or equal to the opponent's piece's height.</summary>
    ///<returns>True if invalid, false is valid.</returns>
    let isDiagonallyInvalid boardState (move:Move) =
        let heights = [for i in 1 .. boardState.MaxHeight-> i]
        let (Move c) = move
        let diagonals = Coordinate.applyInner (fun((a:int, b:int)) -> [for height in heights -> [(a + height, b + height); (a - height, b + height); (a + height, b - height); (a - height, b - height)]]) c
        let predicate = match boardState.WhoseTurn with
        |Player White -> fun(height:int) -> fun(square:int) -> a <= -1 * height
        |Player Black -> fun(height:int) -> fun(square:int) -> a >= height 
        |GameOver     -> fun(height:int) -> fun(square:int) ->false



    type MoveResult = Success of BoardState | TowerTooHighError | GameOverError| OutOfBoundsError
    let makeMove boardState move = 
        if isOutOfBounds boardState move
        then OutOfBoundsError
        else match (boardState.WhoseTurn, isTooTall boardState move) with
        |(GameOver, _) -> GameOverError
        |(Player _, true) -> TowerTooHighError
        |(Player _, false) -> Success boardState