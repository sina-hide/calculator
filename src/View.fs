module Calculator.View

open Calculator.Model
open Fable.Helpers.React
open Fable.Helpers.React.Props

let view model dispatch =
    div
        []
        [
            str (display model)
            table
                []
                [
                    tr
                        []
                        [
                            td
                                []
                                [
                                    button
                                        [OnClick (fun _ -> dispatch (KeyPress (Digit 7)))]
                                        [str "7"]
                                    button
                                        [OnClick (fun _ -> dispatch (KeyPress (Digit 8)))]
                                        [str "8"]
                                    button
                                        [OnClick (fun _ -> dispatch (KeyPress (Digit 9)))]
                                        [str "9"]
                                    button
                                        [OnClick (fun _ -> dispatch (KeyPress ClearKey))]
                                        [str "C"]
                                ]
                        ]
                    tr
                        []
                        [
                            td
                                []
                                [
                                    button
                                        [OnClick (fun _ -> dispatch (KeyPress (Digit 4)))]
                                        [str "4"]
                                    button
                                        [OnClick (fun _ -> dispatch (KeyPress (Digit 5)))]
                                        [str "5"]
                                    button
                                        [OnClick (fun _ -> dispatch (KeyPress (Digit 6)))]
                                        [str "6"]
                                    button
                                        [OnClick (fun _ -> dispatch (KeyPress (Operator Division)))]
                                        [str "/"]
                                ]
                        ]
                    tr
                        []
                        [
                            td
                                []
                                [
                                    button
                                        [OnClick (fun _ -> dispatch (KeyPress (Digit 1)))]
                                        [str "1"]
                                    button
                                        [OnClick (fun _ -> dispatch (KeyPress (Digit 2)))]
                                        [str "2"]
                                    button
                                        [OnClick (fun _ -> dispatch (KeyPress (Digit 3)))]
                                        [str "3"]
                                    button
                                        [OnClick (fun _ -> dispatch (KeyPress (Operator Multiplication)))]
                                        [str "*"]
                                ]
                        ]
                    tr
                        []
                        [
                            td
                                []
                                [
                                    button
                                        [OnClick (fun _ -> dispatch (KeyPress (Digit 0)))]
                                        [str "0"]
                                    button
                                        [OnClick (fun _ -> dispatch (KeyPress EqualKey))]
                                        [str "="]
                                    button
                                        [OnClick (fun _ -> dispatch (KeyPress (Operator Addition)))]
                                        [str "+"]
                                    button
                                        [OnClick (fun _ -> dispatch (KeyPress (Operator Subtraction)))]
                                        [str "-"]
                                ]
                        ]
                ]
        ]
