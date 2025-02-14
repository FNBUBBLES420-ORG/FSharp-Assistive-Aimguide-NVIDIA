module GPUProcessing

open System
open System.Linq
open Microsoft.ML.OnnxRuntime
open Microsoft.ML.OnnxRuntime.Tensors
open ManagedCuda  // NVIDIA CUDA
open ManagedCuda.BasicTypes
open DirectML.NET // AMD DirectML

/// Detect if an NVIDIA GPU is available
let hasNvidiaGpu () =
    try
        CudaContext.GetDeviceCount() > 0
    with
    | :? CudaException -> false
    | _ -> false

/// Detect if an AMD GPU is available
let hasAmdGpu () =
    try
        let devices = DirectML.Device.GetAvailableAdapters()
        devices.Length > 0
    with
    | _ -> false

/// Detect and return the available GPU type
let detectGpuType () =
    if hasNvidiaGpu() then
        "NVIDIA"
    elif hasAmdGpu() then
        "AMD"
    else
        "CPU"

/// Process tensor data on GPU based on detected hardware
let processOnGpu (data: float32[]) =
    match detectGpuType() with
    | "NVIDIA" ->
        printfn "✅ Using NVIDIA CUDA for processing."
        let gpuMem = new CudaDeviceVariable<float32>(data.Length)
        gpuMem.CopyToDevice(data)
        gpuMem
    | "AMD" ->
        printfn "✅ Using AMD DirectML for processing."
        use session = new InferenceSession("yolov8s.onnx", SessionOptions())
        let tensor = new DenseTensor<float32>(data, [|data.Length|])
        let input = NamedOnnxValue.CreateFromTensor("input", tensor)
        use results = session.Run([|input|])
        results.First().AsTensor<float32>()
    | "CPU" ->
        printfn "⚠️ No compatible GPU found! Using CPU instead."
        data // Just return the original data if CPU fallback
