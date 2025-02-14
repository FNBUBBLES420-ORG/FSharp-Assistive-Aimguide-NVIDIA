module GameSelection

open System
open System.Diagnostics
open System.Linq

let gameSelection () =
    let processes = Process.GetProcesses()
    let indexedWindows = 
        processes 
        |> Array.mapi (fun i p -> (i, p.ProcessName))
        |> Array.filter (fun (_, name) -> name <> "")

    if indexedWindows.Length = 0 then
        printfn "No active windows found. Try again."
        None
    else
        indexedWindows |> Array.iter (fun (i, name) -> printfn "[%d]: %s" i name)

        printfn "Enter the number of the window you want to select:"
        let userInput = Console.ReadLine() |> Int32.TryParse

        match userInput with
        | true, index when index < indexedWindows.Length ->
            let processName = indexedWindows.[index] |> snd
            printfn "Selected: %s" processName
            Some processName
        | _ -> 
            printfn "Invalid input. Try again."
            None
