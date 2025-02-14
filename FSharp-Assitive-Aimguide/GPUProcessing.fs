module GPUProcessing

open ManagedCuda
open ManagedCuda.VectorTypes

let device = new CudaContext()

let toCudaTensor (tensor: float32[]) =
    let gpuMem = new CudaDeviceVariable<float32>(tensor.Length)
    gpuMem.CopyToDevice(tensor)
    gpuMem

let runWithCuda (data: float32[]) =
    let gpuData = toCudaTensor data
    printfn "Processed data on GPU!"
