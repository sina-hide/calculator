module App

open Elmish
open Elmish.React

Program.mkSimple Calculator.Model.init Calculator.Update.update Calculator.View.view
|> Program.withReact "elmish-app"
|> Program.withConsoleTrace
|> Program.run
