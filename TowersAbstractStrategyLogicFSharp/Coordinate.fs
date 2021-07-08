namespace GameLogic

module Coordinate =
    
    type T = private Coordinate of Length.T * Length.T
        
    let create (width, height) =
        let width' = Length.create width
        let height' = Length.create height
        match (width', height') with
        | (_, None) -> None 
        | (None, _) -> None
        | (_, _) -> Some(Coordinate (Option.get width', Option.get height'))
    
    let apply f (Coordinate (w, h)) = f (w, h)

    let applyInner f (Coordinate (w, h)) = f (Length.value w, Length.value h)

    let value d = apply id d

    let valueInner d = applyInner id d