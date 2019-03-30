module Calculator.View

open Calculator.Model
open Fable.Helpers.React
open Fable.Helpers.React.Props

let keyLayout =
    [
        ["7", Digit 7;  "8", Digit 8;  "9", Digit 9;            "C", ClearKey]
        ["4", Digit 4;  "5", Digit 5;  "6", Digit 6;            "/", Operator Division]
        ["1", Digit 1;  "2", Digit 2;  "3", Digit 3;            "*", Operator Multiplication]
        ["0", Digit 0;  "=", EqualKey; "+", Operator Addition;  "-", Operator Subtraction]
    ]

let viewButton (caption, key) dispatch =
    button
        [
            OnClick (fun _ -> dispatch (KeyPress key))
            Style [
                Margin "0 auto"
                Padding "0"
            ]
        ]
        [
            div
                [
                    Style [
                        Height 40
                        Width 40
                        Display "table-cell"
                        TextAlign "center"
                        VerticalAlign "middle"
                        FontSize 20
                        Margin "0 auto"
                        Padding "0"
                    ]
                ]
                [str caption]
        ]

let viewCell keyCell dispatch =
    td [] [viewButton keyCell dispatch]

let viewRow keyRow dispatch =
    let elements = keyRow |> List.map (fun keyCell -> viewCell keyCell dispatch)
    tr [] elements

let viewTable keyTable dispatch =
    let elements = keyTable |> List.map (fun keyRow -> viewRow keyRow dispatch)
    table [] [tbody [] elements]

let view model dispatch =
    div
        []
        [
            div
                [Style [FontSize 20]]
                [str (display model)]
            viewTable keyLayout dispatch
        ]
