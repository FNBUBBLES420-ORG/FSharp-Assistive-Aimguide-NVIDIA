@echo off
chcp 65001 >nul 2>&1  REM Enable UTF-8 for better output
cls

echo ===============================
echo  🚀 Installing Required Packages...
echo ===============================

:: Check if .NET SDK is installed
dotnet --version > nul 2>&1
if %errorlevel% neq 0 (
    echo ❌ .NET SDK is not installed. Please install .NET SDK from:
    echo    https://dotnet.microsoft.com/en-us/download
    pause
    exit /b 1
)

setlocal enabledelayedexpansion

:: Define the list of packages
set "packages=Microsoft.ML.OnnxRuntime Microsoft.ML.OnnxRuntime.Gpu TorchSharp MathNet.Numerics Emgu.CV WindowsInput ManagedCuda"

:: Create a log file for tracking failed installations
set "logfile=install_log.txt"
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
    timeout /t 1 > nul  REM Simulate progress delay
)

echo ===============================
echo  🎉 All dependencies installed successfully!
echo ===============================
pause
