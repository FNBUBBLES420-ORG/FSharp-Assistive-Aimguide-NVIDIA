module ScreenCapture

open System
open System.Drawing
open SharpDX
open SharpDX.Direct3D11
open SharpDX.DXGI
open System.Threading.Tasks

let factory = new Factory1()
let adapter = factory.GetAdapter1(0)  // Primary GPU
let device = new Device(adapter)
let output = adapter.GetOutput(0)
let outputDuplication = output.QueryInterface<Output1>().DuplicateOutput(device)

let captureScreenAsync () =
    task {
        let frameInfo = new OutputDuplicateFrameInformation()
        use texture = new Texture2D(device, new Texture2DDescription(
            Width = 1920,
            Height = 1080,
            MipLevels = 1,
            ArraySize = 1,
            Format = Format.B8G8R8A8_UNorm,
            Usage = ResourceUsage.Staging,
            BindFlags = BindFlags.None,
            CpuAccessFlags = CpuAccessFlags.Read,
            OptionFlags = ResourceOptionFlags.None
        ))

        let! _ = Task.Run(fun () ->
            outputDuplication.AcquireNextFrame(500, frameInfo, texture)
        )

        return texture
    }
