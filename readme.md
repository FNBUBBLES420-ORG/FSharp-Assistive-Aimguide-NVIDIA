# 🎯 Assistive Aim Guide (F# YOLO-Based AI)

🚀 **An AI-powered aim assistance tool designed for accessibility, featuring human-like movement and real-time object tracking.**

- **Smooth, non-locking mouse movement** for a natural feel.
- **Supports NVIDIA GPUs** for high-performance inference.
- **Real-time object detection** powered by **Ultralytics YOLO models**.
- **FPS & latency monitoring** for performance insights.
- **Dynamic YOLO model selection via UI** at runtime.
- **Windows 11 compatibility** (requires DirectX and Windows-specific libraries).
- **Supports ONNX models (`.onnx`)** for AI inference.
- **Arduino Leonardo support** for serial input-based control.

---

## 🛠 Features

- **AI-driven object detection** (detects players, objects, etc.).
- **Human-like aiming behavior** (no snap-locks or robotic movements).
- **Optimized for Windows 11** with **NVIDIA CUDA acceleration**.
- **Real-time screen capture & automatic target detection**.
- **Configurable settings** for sensitivity, reaction time, and detection thresholds.
- **Supports multiple YOLO models** (Ultralytics and custom ONNX models).
- **Live FPS & latency tracking** displayed in the console.
- **Interactive UI for selecting YOLO models** at startup.
- **ONNX-only AI model support** (TensorRT `.engine` models are not supported).
- **Arduino Leonardo integration** for real-time serial control. ` Recommended `

---

## 📥 Installation Guide

### **Step 1: Install Required Dependencies**

#### 1️⃣ **Install .NET 8.0 LTS**

Ensure you have the **.NET 8.0 SDK** installed.  ***LONG TERM SUPPORT***
👉 [Download .NET SDK](https://dotnet.microsoft.com/en-us/download)  
Verify the installation by running:
```sh
dotnet --version
```

## 2️⃣ Install Project Dependencies
Run the setup script:
```
setup_dependencies.bat  # (Windows 11)
```
- Or manually install the required packages:

```
dotnet add package Microsoft.ML.OnnxRuntime --version 1.20.1
dotnet add package Microsoft.ML.OnnxRuntime.Gpu --version 1.20.1
dotnet add package TorchSharp --version 0.101.0
dotnet add package MathNet.Numerics --version 5.0.0
dotnet add package Emgu.CV --version 4.6.0.5131
dotnet add package ManagedCuda --version 10.0.0
dotnet add package System.IO.Ports --version 7.0.0
dotnet add package InputSimulatorStandard --version 1.0.0
dotnet add package RJCP.SerialPortStream --version 3.0.1
dotnet add package SharpDX --version 4.2.0
dotnet add package SharpDX.Direct3D11 --version 4.2.0
dotnet add package SharpDX.DXGI --version 4.2.0
dotnet add package Newtonsoft.Json --version 13.0.3
```

## 3️⃣ Configure Execution Policy for PowerShell
Open PowerShell as Administrator, navigate to the directory containing `install.ps1`, and run:
```
Set-ExecutionPolicy Bypass -Scope Process -Force
.\install.ps1
```
- click Y to continue if is asked you if you want to proceed.

### make sure to `CD` the `src` folder before doing below: 
## 🔧 Build and Run the Project
1️⃣ Restore Dependencies
```
dotnet restore
```

## 2️⃣ Compile the Project
```
dotnet build
```

## 3️⃣ Launch the Assistive Aim Guide
```
dotnet run
```
- Upon launch, a UI will prompt you to select a YOLO model before the tracking system starts.

### 🎮 How It Works
1. Select the YOLO model using the UI at launch.
2. Select your game window (or use the primary monitor for capture).
3. The AI detects objects in real-time and calculates the required movement.
4. The mouse moves naturally toward detected objects (no snap-locks).
5. FPS & latency are monitored in real-time and displayed in the console.
6. Exit the tool at any time by pressing the configured key.

## ⚙️ Configuration
You can edit the config.json file to tweak settings such as:

- Model file (`model_path`)
- Sensitivity (`mouse_speed`)
- Reaction delay (`reaction_time`)
- Detection confidence (`detection_threshold`)
- Enabled features (`visuals`, `debug_mode`)

```
{
  "model_path": "yolov8m.onnx",
  "mouse_speed": 1.5,
  "reaction_time": 50,
  "detection_threshold": 0.4,
  "visuals": true,
  "debug_mode": false,
  "gpu_id": 0
}
```

## 🔧 Troubleshooting
- Issue: Tool not detecting objects?
- Fix: Ensure your screen capture or camera is working and properly positioned.
---
- Issue: Mouse movements feel robotic.
- Fix: Adjust mouse_speed and reaction_time in config.json.
---
- Issue: Performance is slow.
- Fix: Verify you have an NVIDIA GPU with CUDA support and check the FPS monitor.
---
- Issue: Arduino Leonardo not recognized.
- Fix: Ensure the correct COM port is set and the drivers are properly installed.
---

### Note: This tool is Windows 11-only due to its reliance on DirectX and Windows-specific libraries.

## 📄 License
This project is proprietary. All rights are reserved by the author. Unauthorized copying, distribution, or modification is strictly prohibited unless you have written permission from the Developer or the FNBUBBLES420 ORG.

## 📧 Need help? Contact us on Discord or open an issue!

[Invite Link](https://discord.fnbubbles420.org/invite)

- Go to the `Assistive AimGuide channel` and `ping Bubbles The Dev`.

## 🚀 Happy Gaming! 🎮
