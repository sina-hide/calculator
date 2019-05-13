module Calculator.View

open Calculator.Model
open Elmish
open Fable.Core
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

let additionalShortcutKeys = [
    "c", ClearKey
    "Escape", ClearKey
    "Enter", EqualKey
]

let shortcutCodes = [
    "Numpad0", Digit 0
    "Numpad1", Digit 1
    "Numpad2", Digit 2
    "Numpad3", Digit 3
    "Numpad4", Digit 4
    "Numpad5", Digit 5
    "Numpad6", Digit 6
    "Numpad7", Digit 7
    "Numpad8", Digit 8
    "Numpad9", Digit 9
    "NumpadDivide", Operator Division
    "NumpadMultiply", Operator Multiplication
    "NumpadSubtract", Operator Subtraction
    "NumpadAdd", Operator Addition
    "NumpadEnter", EqualKey
    //"NumpadDecimal", ...
]

let shortcutKeyMap =
    let layoutDerived = keyLayout |> Seq.concat
    let allShortcutKeys = Seq.append layoutDerived additionalShortcutKeys
    allShortcutKeys |> Map.ofSeq

let shortcutCodeMap = Map.ofSeq shortcutCodes

[<Emit("$0.code")>]
let getShortcutEventCode (shortcut:Browser.KeyboardEvent) : string =
    jsNative

let handleShortcut (shortcut:Browser.KeyboardEvent) dispatch =
    let code = getShortcutEventCode shortcut
    let key = shortcut.key
    let found =
        shortcutCodeMap.TryFind code
        |> Option.orElse (shortcutKeyMap.TryFind key)
    match found with
    | Some key ->
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
    let elements = keyRow |> Seq.map (fun keyCell -> viewCell keyCell dispatch)
    tr [] elements

let viewTable keyTable dispatch =
    let elements = keyTable |> Seq.map (fun keyRow -> viewRow keyRow dispatch)
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
