module ModelSelection

open System
open System.IO
open System.Windows.Forms
open Newtonsoft.Json

type Config = { model_path: string }

let configFile = "config.json"

let loadConfig () =
    if File.Exists(configFile) then
        JsonConvert.DeserializeObject<Config>(File.ReadAllText(configFile))
    else
        { model_path = "yolov8s.onnx" }

let saveConfig modelPath =
    let config = { model_path = modelPath }
    File.WriteAllText(configFile, JsonConvert.SerializeObject(config, Formatting.Indented))

let showModelSelectionUI () =
    let form = new Form(Text = "Select YOLO Model", Width = 400, Height = 200)
    let dropdown = new ComboBox(Dock = DockStyle.Top)
    dropdown.Items.AddRange([|"yolov5s.onnx"; "yolov8s.onnx"; "custom_model.onnx"|])
    dropdown.SelectedIndex <- 0

    let button = new Button(Text = "Select", Dock = DockStyle.Bottom)
    button.Click.Add(fun _ ->
        let selectedModel = dropdown.SelectedItem.ToString()
        saveConfig selectedModel
        MessageBox.Show(sprintf "Model selected: %s" selectedModel, "YOLO Model", MessageBoxButtons.OK)
        form.Close()
    )

    form.Controls.Add(dropdown)
    form.Controls.Add(button)
    form.ShowDialog()

