namespace GameLogic

module Dimension =

    type T = private Dimension of Length.T * Length.T
    
    let create (width, height) =
        let width' = Length.create width
        let height' = Length.create height
        match (width', height') with
        | (_, None) -> None 
        | (None, _) -> None
        | (_, _) -> Some(Dimension (Option.get width', Option.get height'))

    let apply f (Dimension (w, h)) = f (w, h)

    let applyInner f (Dimension (w, h)) = f (Length.value w, Length.value h)

    let value d = apply id d
