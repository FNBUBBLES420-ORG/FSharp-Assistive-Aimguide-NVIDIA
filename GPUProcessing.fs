module GPUProcessing

open System
open Microsoft.ML.OnnxRuntime
open Microsoft.ML.OnnxRuntime.Tensors
open ManagedCuda  // NVIDIA CUDA
open ManagedCuda.BasicTypes

/// Detect if an NVIDIA GPU is available
let hasNvidiaGpu () =
    try
        CudaContext.GetDeviceCount() > 0
    with
    | :? CudaException -> false
    | _ -> false

/// Detect and return the available GPU type
let detectGpuType () =
    if hasNvidiaGpu() then
        "NVIDIA"
    else
        "CPU"

/// Process tensor data on GPU based on detected hardware
let processOnGpu (data: float32[]) =
    match detectGpuType() with
    | "NVIDIA" ->
        printfn "✅ Using NVIDIA CUDA for processing."
        let gpuMem = new CudaDeviceVariable<float32>(data.Length)
        gpuMem.CopyToDevice data
        gpuMem
    | "CPU" ->
        printfn "⚠️ No compatible GPU found! Using CPU instead."
        (new CudaDeviceVariable<float32>(data.Length)).CopyToDevice data
        let cpuMem = new CudaDeviceVariable<float32>(data.Length)
        cpuMem.CopyToDevice data
        cpuMem
    | _ ->
        printfn "⚠️ Unknown GPU type detected! Using CPU instead."
        let unknownGpuMem = new CudaDeviceVariable<float32>(data.Length)
        unknownGpuMem.CopyToDevice data
        unknownGpuMem
