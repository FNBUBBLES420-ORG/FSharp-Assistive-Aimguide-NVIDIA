@echo off
chcp 65001 >nul 2>&1  REM Ensure UTF-8 encoding for compatibility, check Windows version if needed  REM Enable UTF-8 for better output
cls

echo ===============================
echo  🚀 Installing Required Packages...
echo ===============================

:: Check if .NET SDK is installed
dotnet --version 2> dotnet_version.log
if %errorlevel% neq 0 (
    echo ❌ .NET SDK is not installed. Please install .NET SDK from:
    echo ✅ .NET 8.0 Long Term Support
    echo 👉 https://dotnet.microsoft.com/en-us/download
    if not "%CI%"=="true" pause
    exit /b 1
)

setlocal enabledelayedexpansion

:: Define the list of packages
set "packages=Microsoft.ML.OnnxRuntime --version 1.20.1
Microsoft.ML.OnnxRuntime.Gpu --version 1.20.1
TorchSharp --version 0.101.0
MathNet.Numerics --version 5.0.0
Emgu.CV --version 4.6.0.5131
ManagedCuda --version 10.0.0
System.IO.Ports --version 7.0.0
InputSimulatorStandard --version 1.0.0
RJCP.SerialPortStream --version 3.0.1
SharpDX --version 4.2.0
SharpDX.Direct3D11 --version 4.2.0
SharpDX.DXGI --version 4.2.0
Newtonsoft.Json --version 13.0.3"

:: Create a log file for tracking failed installations
set "logfile=install_log.txt"
if exist "%logfile%" (echo. >> "%logfile%")
echo Install Log - %DATE% %TIME% > "%logfile%"

for %%p in (%packages%) do (
    echo Installing: %%p...
    dotnet add package %%p >> "%logfile%" 2>&1
    if %errorlevel% neq 0 (
        echo ❌ Failed to install: %%p (Check install_log.txt for details)
        if not "%CI%"=="true" pause
        exit /b 1
    )
    echo ✅ Successfully installed: %%p
    rem timeout can be adjusted or removed for automated scripts  REM Simulate progress delay
)

echo ===============================
echo  🎉 All dependencies installed successfully!
echo ===============================
if not "%CI%"=="true" pause
