module AssistiveAimGuide

open System
open System.Diagnostics
open System.Threading.Tasks
open RJCP.IO.Ports
open WindowsInput
open ScreenCapture
open YOLOInference
open InputControl

let simulator = new InputSimulator()
let rand = Random()
let mutable frameCount = 0
let mutable lastTime = Stopwatch.StartNew()

// Display Help Message
let showHelpMessage () =
    printfn "📧 Need help? Join FNBubbles420 Org Community Discord Server"
    printfn "🔗 Invite Link: https://discord.fnbubbles420.org/invite"
    printfn "📌 Go To Assistive AimGuide Channel and ping @Bubbles The Dev"
    printfn "=========================================================="

// Show help message at startup
showHelpMessage ()

// Serial Communication Setup for Arduino Leonardo
let port = new SerialPortStream("COM3", 115200)  // Replace COM3 with your actual port
try
    port.Open()
    printfn "✅ Connected to Arduino Leonardo!"
with
| ex -> printfn "❌ Error opening serial port: %s" ex.Message

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

        // Process Serial Input from Arduino Leonardo
        if port.IsOpen && port.BytesToRead > 0 then
            let data = port.ReadLine().Trim()
            printfn "📡 Received from Arduino: %s" data
            match data with
            | "ENABLE_AIM" -> printfn "🎯 Assistive Aim Enabled"
            | "DISABLE_AIM" -> printfn "🛑 Assistive Aim Disabled"
            | "RESET" -> printfn "🔄 Resetting Aim Assist"
            | "HELP" -> showHelpMessage()  // Users can request help via serial input
            | _ -> printfn "⚠️ Unknown command: %s" data
        
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

// Ensure Serial Port is properly closed on exit
System.AppDomain.CurrentDomain.ProcessExit.Add(fun _ ->
    if port.IsOpen then port.Close()
    printfn "🔌 Serial Port Closed"
)
