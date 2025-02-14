@echo off
echo Adding required NuGet packages...

dotnet add package Microsoft.ML.OnnxRuntime
dotnet add package Microsoft.ML.OnnxRuntime.Gpu
dotnet add package TorchSharp
dotnet add package MathNet.Numerics
dotnet add package Emgu.CV
dotnet add package WindowsInput
dotnet add package ManagedCuda

echo All dependencies installed successfully!
pause
