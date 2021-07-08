namespace GameLogic

module Move =
    open DomainTypes
    open BoardState
    
    let private unwrapMove move =
        let (Move m) = move
        Coordinate.valueInner m

    let isOutOfBounds boardState move = 
        let move' = unwrapMove move
        Array2D.base1 boardState.State > fst move' && Array2D.base2 boardState.State > snd move'
    
    /// <summary>Checks if the move is valid based on height rules.
    /// A move cannot be played higher than the board's max height (A piece is height one when initially placed and each subsequent placement on the same tile increases height by one).</summary>
    ///<returns>True if invalid, false is valid.</returns>
    let isTooTall boardState move = 
        match boardState.WhoseTurn with
        |Player White -> tryApplyByCoordinate boardState (fun (height) -> height >= boardState.MaxHeight) (unwrapMove move) false
        |Player Black -> tryApplyByCoordinate boardState (fun (height) -> height <= -1 * boardState.MaxHeight) (unwrapMove move) false
        |GameOver     -> false

    let private getDiagonals (a,b) height =
        [(a + height, b + height); (a - height, b + height); (a + height, b - height); (a - height, b - height)]

    let private anyTrue (list:list<bool>) = 
        List.exists ((=) true) list

    /// <summary>Checks if the move is valid based on diagonal rules.
    /// A move cannot be played at a diagonal distance from an opponents piece less than or equal to the opponent's piece's height.</summary>
    ///<returns>True if invalid, false is valid.</returns>
    let isDiagonallyInvalid boardState (move:Move) =
        let heights = [for i in 1 .. boardState.MaxHeight-> i]
        let (Move c) = move
        let predicate = match boardState.WhoseTurn with
                        |Player White -> fun(height:int) -> fun(square:int) -> square <= -1 * height
                        |Player Black -> fun(height:int) -> fun(square:int) -> square >= height 
                        |GameOver     -> fun(height:int) -> fun(square:int) ->false
        let isTrueByHeight = [for height in heights ->
                              let predicate' = predicate height
                              let diagonalCoords = Coordinate.applyInner (fun((a:int, b:int)) -> getDiagonals (a,b) height) c
                              let booleans = [for coord in diagonalCoords -> tryApplyByCoordinate boardState predicate' coord false]
                              anyTrue booleans]
        anyTrue isTrueByHeight

    type MoveResult = Success of BoardState | TowerTooHighError | DiagonalBlockError | GameOverError| OutOfBoundsError
    
    /// <summary>Forcibly makes the given move assuming it is valid</summary>
    ///<returns>True if invalid, false is valid.</returns>
    let private forceMakeMove (player:Player) (boardState:BoardState) (move:Move):BoardState =
        let coords = unwrapMove move
        let pred = match player with
                   |White -> fun square -> let (SquareState s) = square
                                           match s with
                                           |s when s < 0 -> 1
                                           |_ -> s + 1
                   |Black -> fun square -> let (SquareState s) = square
                                           match s with
                                           |s when s > 0 -> -1
                                           |_ -> s - 1
        let mainPred = fun x -> fun y -> fun square -> match x,y with 
                                                       |x,y when (x,y) = coords -> SquareState (pred square)
                                                       |(_,_) -> square
        let whoseTurn' = match boardState.WhoseTurn with
                         |Player White -> Player White
                         |Player Black -> Player Black
                         |GameOver -> GameOver              
        {WhoseTurn = whoseTurn'; State = Array2D.mapi mainPred boardState.State; MaxHeight = boardState.MaxHeight}
    
    /// <summary>Makes a move and returns the new state if its a valid move
    /// Returns one of many error states if the move is invalid</summary>
    ///<returns>True if invalid, false is valid.</returns>
    let makeMove boardState move = 
        if isOutOfBounds boardState move
        then OutOfBoundsError
        else match (boardState.WhoseTurn, isTooTall boardState move, isDiagonallyInvalid boardState move) with
             |(GameOver, _, _) -> GameOverError
             |(Player _, true, _) -> TowerTooHighError
             |(Player _, _, true) -> DiagonalBlockError
             |(Player p, false, false) -> Success (forceMakeMove p boardState move)