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
    echo 👉 Follow The Directions When Installing Dotnet Program.
    echo 👉 Once the dotnet is successfully installed Read Below:
    echo ===============================
    echo  🎉 Close Terminal - Press Enter & CD the src folder for the dotnet packages
    echo ===============================
    if not "%CI%"=="true" pause
    exit /b 1
)
