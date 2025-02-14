module YOLOInference

open System
open System.IO
open Microsoft.ML.OnnxRuntime
open Microsoft.ML.OnnxRuntime.Tensors
open Newtonsoft.Json
open System.Threading.Tasks

type Config = { model_path: string }
let configFile = "config.json"

// Load Configuration
let config = 
    if File.Exists(configFile) then
        JsonConvert.DeserializeObject<Config>(File.ReadAllText(configFile))
    else
        { model_path = "yolov8s.onnx" }

printfn "Loading YOLO model: %s" config.model_path

let options = SessionOptions()
options.AppendExecutionProvider_CUDA(0)  // Enable GPU Execution

let session = new InferenceSession(config.model_path, options)

let confidenceThreshold = 0.4

let preprocessImage (imageData: byte[]) =
    let tensor = DenseTensor<float32>(Array.map float32 imageData, [|1; 3; 320; 320|])
    tensor

let runInferenceAsync (imageData: byte[]) =
    task {
        let inputTensor = preprocessImage imageData
        let inputName = session.InputMetadata.Keys |> Seq.head
        let inputs = Map.ofList [(inputName, inputTensor :> IDisposable)]
        let! results = Task.Run(fun () -> session.Run(inputs))
        return results |> Seq.map (fun result -> result.AsTensor<float32>().ToArray()) |> Seq.toList
    }
