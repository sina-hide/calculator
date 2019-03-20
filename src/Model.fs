module Calculator.Model

type Operator =
    | Addition
    | Subtraction
    | Multiplication
    | Division

type Result =
    | Empty
    | Result of decimal
    | Error

type Model = {
    Result : Result;
    Operator : Operator option;
    Operand : decimal option;
 }

type Key =
    | ClearKey
    | Digit of int
    | Operator of Operator
    | EqualKey

type Message = KeyPress of Key

let init () = { Result = Empty; Operator = None; Operand = None }

let display model =
    match model.Operand with
    | None ->
        match model.Result with
        | Empty -> "0"
        | Result result -> sprintf "%A" result
        | Error -> "Error"
    | _ ->
        match model.Operand with
        | None -> "0"
        | Some operand -> sprintf "%A" operand
