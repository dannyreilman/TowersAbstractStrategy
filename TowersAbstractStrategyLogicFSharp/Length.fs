namespace GameLogic 

module Length =
    type T = private Length of int
    
    let create length =
        if (length > 0)
        then Some (Length length)
        else None
        
    let apply f (Length length) = f length

    let value length = apply id length
