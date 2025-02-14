# 🎯 Assistive Aim Guide (F# YOLO-Based AI)
🚀 **A powerful, AI-driven aim assistance tool designed for accessibility, using human-like movements and real-time object tracking.**  
✅ **Non-locking, smooth mouse movement** for a natural feel.  
✅ **Supports NVIDIA & AMD GPUs** for maximum performance.  
✅ **Detects objects in real-time** using **Ultralytics YOLO models**.  
✅ **Real-time FPS & latency monitoring** for performance insights.  
✅ **Dynamic YOLO model selection via UI** at runtime.  
✅ **Windows 11 compatibility** (requires DirectX and Windows-specific libraries).  
✅ **Supports only ONNX models (`.onnx`)** for AI inference.  
✅ **Supports Arduino Leonardo for serial input-based control.**  

---

## 🛠 **Features**
✔ **AI-powered object detection** (supports people, objects, etc.)  
✔ **Smooth, human-like aiming behavior** (non-locking, no "snap" movement)  
✔ **Optimized for Windows 11 with NVIDIA CUDA & AMD DirectML support**  
✔ **Real-time camera feed & auto-detection**  
✔ **Configurable reaction time, movement sensitivity**  
✔ **Supports multiple YOLO models from Ultralytics**  
✔ **Real-time FPS & latency tracking** displayed in the console  
✔ **Interactive UI for selecting YOLO models**  
✔ **Windows 11-only compatibility** (uses WindowsInput, DirectX, and SharpDX for screen capture).  
✔ **ONNX-only AI model support** (TensorRT `.engine` models are not supported).  
✔ **Arduino Leonardo support for reading serial input.**  

---

## 📥 **Installation**
### **Step 1: Install Dependencies**
Ensure you have the **.NET SDK** installed:  
👉 [Download .NET SDK](https://dotnet.microsoft.com/en-us/download)  

Run the following command to install required packages:
```sh
setup_dependencies.bat  # (Windows 11)
```
Or manually install dependencies:
```sh
dotnet add package Microsoft.ML.OnnxRuntime
dotnet add package Microsoft.ML.OnnxRuntime.Gpu
dotnet add package TorchSharp
dotnet add package MathNet.Numerics
dotnet add package Emgu.CV
dotnet add package WindowsInput
dotnet add package ManagedCuda
dotnet add package Microsoft.ML.OnnxRuntime.DirectML
dotnet add package DirectML.NET
dotnet add package System.IO.Ports
```

### 📌 install.ps1
- Run the script manually on your system:
- Open PowerShell as `Administrator`.
- Navigate to the directory where the script is located. Run:

```
Set-ExecutionPolicy Bypass -Scope Process -Force
.\install.ps1    <---- Copy Command # Windows 11 Only
```

### **Step 2: Build and Run the Project**
Once you have installed the dependencies, follow these steps:

1️⃣ **Restore dependencies** to ensure all required packages are available:
```sh
dotnet restore
```

2️⃣ **Compile the project** to generate the executable:
```sh
dotnet build
```

3️⃣ **Launch the Assistive Aim Guide**:
```sh
dotnet run
```

Upon launch, a UI will prompt you to select a YOLO model before the tracking system starts.

---

## 🎮 **How It Works**
1️⃣ **Select the YOLO model** using the UI at launch.  
2️⃣ **Select your game window** (or use the primary monitor for capture).  
3️⃣ **The AI detects objects in real-time** and calculates movement.  
4️⃣ **The mouse moves naturally** toward detected objects (no snap locks).  
5️⃣ **Monitor FPS & latency** updates in the console for performance insights.  
6️⃣ **Exit the tool anytime** by pressing the configured key.  

---

## ⚙️ **Configuration**
You can edit `config.json` to tweak settings like:
- **Model file** (`model_path`)
- **Sensitivity** (`mouse_speed`)
- **Reaction delay** (`reaction_time`)
- **Detection confidence** (`detection_threshold`)
- **Enabled features** (`visuals`, `debug_mode`)

Example `config.json`:
```json
{
  "model_path": "yolov8s.onnx",
  "mouse_speed": 1.5,
  "reaction_time": 50,
  "detection_threshold": 0.4,
  "visuals": true,
  "debug_mode": false
}
```

---

## 🔧 **Troubleshooting**
💡 **Issue:** Tool not detecting objects?  
✔ **Fix:** Ensure your camera or screen capture is working and properly positioned.

💡 **Issue:** Mouse movements feel robotic?  
✔ **Fix:** Adjust `mouse_speed` and `reaction_time` in `config.json`.

💡 **Issue:** Performance is slow?  
✔ **Fix:** Ensure you have an **NVIDIA or AMD GPU** with CUDA/DirectML support and check FPS monitoring.

💡 **Issue:** Running on Linux or macOS?  
✔ **Fix:** This tool is **Windows 11-only** due to dependencies on DirectX and WindowsInput.

💡 **Issue:** TensorRT `.engine` models are not working?  
✔ **Fix:** This project **only supports ONNX (`.onnx`) models** for AI inference.  

💡 **Issue:** Arduino Leonardo is not recognized?  
✔ **Fix:** Ensure the correct COM port is set, and drivers are properly installed.

---

## 📝 **License**
This project is `Private License`.

📧 **Need help?** Contact us on Discord or open an issue!
### [Invite Link](https://discord.fnbubbles420.org/invite)
- Go To `Assistive AimGuide` Channel.

## 🚀 Happy Gaming! 🎮
