# Ensure script runs with administrator privileges
if (-not ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) {
    Write-Host "❌ Please run this script as Administrator!"
    pause
    exit
}

Write-Host "==============================="
Write-Host "🚀 Installing Required Packages..."
Write-Host "==============================="

# Check if .NET SDK is installed
$dotnetVersion = & dotnet --version 2>$null
if (-not $dotnetVersion) {
    Write-Host "❌ .NET SDK is not installed. Please install .NET SDK from:"
    Write-Host "   https://dotnet.microsoft.com/en-us/download"
    pause
    exit
}

# Navigate to the script's directory
Set-Location -Path $PSScriptRoot

# Check if an .fsproj file exists in the directory
$fsprojFile = Get-ChildItem -Path . -Filter "*.fsproj" | Select-Object -First 1
if (-not $fsprojFile) {
    Write-Host "❌ No .fsproj file found! Ensure you are running this script inside a valid F# project directory."
    pause
    exit
}

# Define package list (Added System.IO.Ports for Serial Communication)
$packages = @(
    "Microsoft.ML.OnnxRuntime"
    "Microsoft.ML.OnnxRuntime.Gpu"
    "Microsoft.ML.OnnxRuntime.DirectML"
    "TorchSharp"
    "MathNet.Numerics"
    "Emgu.CV"
    "WindowsInput"
    "ManagedCuda"
    "DirectML.NET"
    "System.IO.Ports"
)

# Create a log file
$logfile = "install_log.txt"
if (Test-Path $logfile) { Clear-Content $logfile }
Add-Content -Path $logfile -Value "Install Log - $(Get-Date)"

# Install packages
foreach ($package in $packages) {
    Write-Host "Installing: $package..."
    $output = & dotnet add package $package 2>&1
    if ($LASTEXITCODE -ne 0) {
        Write-Host "❌ Failed to install: $package (Check install_log.txt for details)"
        Add-Content -Path $logfile -Value "❌ Failed: $package - $output"
        pause
        exit
    }
    Write-Host "✅ Successfully installed: $package"
}

Write-Host "==============================="
Write-Host "🎉 All dependencies installed successfully!"
Write-Host "==============================="

# Check if Serial Ports exist (Modern PowerShell command)
Write-Host "🔍 Detecting Serial Ports..."
$serialPorts = Get-CimInstance Win32_SerialPort | Select-Object DeviceID, Description
if ($serialPorts) {
    $serialPorts | Format-Table -AutoSize
} else {
    Write-Host "⚠️ No serial ports detected. Ensure your Arduino Leonardo is connected."
}

pause
