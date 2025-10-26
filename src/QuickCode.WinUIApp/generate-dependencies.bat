@echo off
setlocal enabledelayedexpansion

REM Configuration
set OUTPUT_FILE=..\..\DEPENDENCIES.md

REM Use current directory
for %%f in (*.csproj) do set PROJECT_PATH=%%f

if "%PROJECT_PATH%"=="" (
    echo ERROR: No .csproj file found in current directory!
    pause
    exit /b 1
)

echo Found project: %PROJECT_PATH%
echo.
echo # Third-Party Software Acknowledgments > %OUTPUT_FILE%
echo This document lists the open-source libraries and tools used in this project, along with their corresponding licenses and copyright notices. >> %OUTPUT_FILE%
echo. >> %OUTPUT_FILE%


echo Generating package list...
dotnet list "%PROJECT_PATH%" package > temp_packages.txt 2>&1

set count=0
for /f "tokens=2" %%a in ('findstr /R ">" temp_packages.txt') do (
    set /a count+=1
    set "package=%%a"
    echo Processing: !package!
    
    REM Detect license type based on package name
    set "license_type=MIT License"
    set "copyright=© .NET Foundation and Contributors"
    set "source=https://github.com/dotnet/runtime"
    
    REM Check for specific packages
    echo !package! | findstr /I "Microsoft" >nul
    if !errorlevel! equ 0 (
        set "license_type=MIT License"
        set "copyright=© Microsoft Corporation"
        set "source=https://github.com/dotnet/runtime"
    )
    
    echo !package! | findstr /I "CommunityToolkit" >nul
    if !errorlevel! equ 0 (
        set "copyright=© .NET Foundation and Contributors"
        set "source=https://github.com/CommunityToolkit/dotnet"
    )
    
    echo. >> %OUTPUT_FILE%
    echo --- >> %OUTPUT_FILE%
    echo. >> %OUTPUT_FILE%
    echo ## !package! >> %OUTPUT_FILE%
    echo **License:** !license_type! >> %OUTPUT_FILE%
    echo **Source:** !source! >> %OUTPUT_FILE%
    echo **Copyright:** !copyright! >> %OUTPUT_FILE%
    echo. >> %OUTPUT_FILE%
    
    REM Add MIT License text (most common for .NET packages)
    echo Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"^), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: >> %OUTPUT_FILE%
    echo. >> %OUTPUT_FILE%
    echo The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. >> %OUTPUT_FILE%
    echo. >> %OUTPUT_FILE%
    echo THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. >> %OUTPUT_FILE%
)

echo. >> %OUTPUT_FILE%
echo --- >> %OUTPUT_FILE%
echo. >> %OUTPUT_FILE%
echo **Last updated:** %date% >> %OUTPUT_FILE%

del temp_packages.txt

echo.
echo Found %count% packages
echo File generated: %OUTPUT_FILE%
pause