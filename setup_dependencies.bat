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
    echo    https://dotnet.microsoft.com/en-us/download
    if not "%CI%"=="true" pause
    exit /b 1
)

setlocal enabledelayedexpansion

:: Define the list of packages
set "packages=Microsoft.ML.OnnxRuntime Microsoft.ML.OnnxRuntime.Gpu Microsoft.ML.OnnxRuntime.DirectML TorchSharp MathNet.Numerics Emgu.CV WindowsInput ManagedCuda DirectML.NET"

:: Create a log file for tracking failed installations
set "logfile=install_log.txt"
if exist "%logfile%" (echo. >> "%logfile%")
echo Install Log - %DATE% %TIME% > "%logfile%"

for %%p in (%packages%) do (
    echo Installing: %%p...
    dotnet add package %%p > "%logfile%" 2>&1
    if %errorlevel% neq 0 (
        echo ❌ Failed to install: %%p (Check install_log.txt for details)
        pause
        exit /b 1
    )
    echo ✅ Successfully installed: %%p
    rem timeout can be adjusted or removed for automated scripts  REM Simulate progress delay
)

echo ===============================
echo  🎉 All dependencies installed successfully!
echo ===============================
pause
