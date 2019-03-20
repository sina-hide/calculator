module Calculator.Update

open System
open Calculator.Model

let clear model =
    match model.Result, model.Operator, model.Operand with
    | _, _, Some _ -> { model with Operand = None }
    | _, Some _, _ -> { model with Operator = None }
    | _, _, _ -> { model with Result = Empty }

let addDigit (digit:int) number = number * 10m + decimal digit

let addDigitToResult (digit:int) = function
    | Empty -> Result (decimal digit)
    | Result result -> Result (addDigit digit result)
    | Error -> Error

let addDigitToOperand (digit:int) = function
    | None -> Some (decimal digit)
    | Some operand -> Some (addDigit digit operand)

let enterDigit digit model =
    match model.Operator with
    | None -> { model with Result = addDigitToResult digit model.Result }
    | Some _ -> { model with Operand = addDigitToOperand digit model.Operand }

let add (a:decimal) (b:decimal) = a + b
let subtract (a:decimal) (b:decimal) = a - b
let multiply (a:decimal) (b:decimal) = a * b
let divide (a:decimal) (b:decimal) = a / b

let evaluate operation model =
    match model.Result with
    | Error -> model
    | _ ->
        let left =
            match model.Result with
            | Result result -> result
            | _ -> 0m
        let right =
            match model.Operand with
            | Some operand -> operand
            | _ -> 0m
        try
            let result = Result (operation left right)
            { Result = result; Operator = None; Operand = None }
        with
        | _ -> { Result = Error; Operator = None; Operand = None }

let equal model =
    match model.Operator with
    | None -> model
    | Some Addition -> evaluate add model
    | Some Subtraction -> evaluate subtract model
    | Some Multiplication -> evaluate multiply model
    | Some Division -> evaluate divide model

let enterOperator operator model =
    let model' =
        match model.Operator with
        | Some _ -> equal model
        | _ -> model
    { model' with Operator = Some operator; Operand = None }

let updateKey = function
    | ClearKey -> clear
    | Digit digit -> enterDigit digit
    | Operator operator -> enterOperator operator
    | EqualKey -> equal

let update = function
    | KeyPress key -> updateKey key
