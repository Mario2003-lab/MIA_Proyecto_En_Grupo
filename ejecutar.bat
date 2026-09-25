@echo off
echo =========================================
echo    Iniciando el Sistema de Archivos...
echo =========================================
echo.

cd Proyecto1

echo [1/2] Compilando el proyecto...
dotnet build
if %errorlevel% neq 0 (
    echo.
    echo [ERROR] Hubo un problema al compilar. Revisa el código.
    pause
    exit /b %errorlevel%
)

echo.
echo [2/2] Ejecutando el programa...
echo =========================================
echo.
dotnet run

echo.
pause