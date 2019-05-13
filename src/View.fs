module Calculator.View

open Calculator.Model
open Elmish
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.Import

let keyLayout =
    [
        [
            "7", Digit 7
            "8", Digit 8
            "9", Digit 9
            "C", ClearKey
        ]
        [
            "4", Digit 4
            "5", Digit 5
            "6", Digit 6
            "/", Operator Division
        ]
        [
            "1", Digit 1
            "2", Digit 2
            "3", Digit 3
            "*", Operator Multiplication
        ]
        [
            "0", Digit 0
            "=", EqualKey
            "+", Operator Addition
            "-", Operator Subtraction
        ]
    ]

let handleShortcut (shortcut:Browser.KeyboardEvent) dispatch =
    let pressedShortcut = shortcut.key
    let foundCell =
        keyLayout
        |> List.concat
        |> List.tryFind (fun (caption, key) -> caption = pressedShortcut)
    match foundCell with
    | Some (_, key) ->
        dispatch (KeyPress key)
        shortcut.preventDefault ()
    | None -> ()

let subscribeShortcuts initial =
    let sub dispatch =
        Browser.window.addEventListener_keydown
            (fun shortcut -> handleShortcut shortcut dispatch)
    Cmd.ofSub sub

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
                        Width 30
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
                [Style [FontSize 30]]
                [str (display model)]
            viewTable keyLayout dispatch
        ]
