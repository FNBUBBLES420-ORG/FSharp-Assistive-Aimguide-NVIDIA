module Main

open System
open System.Diagnostics
open System.Threading.Tasks
open WindowsInput
open ScreenCapture
open YOLOInference
open InputControl

let simulator = new InputSimulator()
let rand = Random()
let mutable frameCount = 0
let mutable lastTime = Stopwatch.StartNew()

let rec mainLoop () =
    task {
        let startTime = Stopwatch.StartNew()

        let! frame = captureScreenAsync ()
        let imageData = frame.ToByteArray()  // Convert DXGI frame to byte array
        let! results = runInferenceAsync imageData
        let detected = results |> List.head

        if detected.Length > 0 then
            let xMid = detected.[0]
            let yMid = detected.[1]
            let moveX = int (xMid - 960.0)  // Assuming 1920x1080 resolution
            let moveY = int (yMid - 540.0)

            let jitterX = rand.Next(-5, 5)
            let jitterY = rand.Next(-5, 5)

            aimAtTarget (moveX + jitterX) (moveY + jitterY)

        frameCount <- frameCount + 1
        let elapsedTime = startTime.ElapsedMilliseconds

        if lastTime.ElapsedMilliseconds >= 1000 then
            printfn "FPS: %d | Latency: %dms" frameCount elapsedTime
            frameCount <- 0
            lastTime.Restart()

        do! mainLoop ()
    }

[<EntryPoint>]
let main argv =
    match gameSelection() with
    | Some _ -> 
        printfn "Starting primary monitor capture..."
        mainLoop () |> Async.StartAsTask |> ignore
    | None -> printfn "No monitor selected."
    0
