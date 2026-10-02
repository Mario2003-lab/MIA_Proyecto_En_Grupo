# MIA Proyecto en Grupo - Gestion de Cursos (Grupo 4)

Sistema de gestion de informacion en C# que guarda los datos en un archivo XML, con encriptacion.
Curso: Manejo e Implementacion de Archivos - URL, 2026S2.

## Integrantes
- Mario Maldonado (coordinador)
- Carlos Pacheco
- Michael Gutierrez
- Luis de Leon
- Victor Perez

## 1. Requisitos

- Windows 10 u 11
- .NET Framework [REVISAR: version, ver Proyecto1.csproj en la etiqueta TargetFramework]
- Git
- Visual Studio 2022 (opcional, solo si se quiere abrir el codigo)

### Verificar si ya esta instalado .NET
Abrir CMD y ejecutar:

```
dotnet --list-runtimes
dotnet --version
```

Si el comando no se reconoce, instalar .NET desde:
https://dotnet.microsoft.com/download

### Verificar Git
```
git --version
```
Si no se reconoce, instalar desde https://git-scm.com/downloads

## 2. Descargar el proyecto

```
git clone https://github.com/Mario2003-lab/MIA_Proyecto_En_Grupo.git
cd MIA_Proyecto_En_Grupo
```

Estructura del proyecto:

```
MIA_Proyecto_En_Grupo/
  ejecutar.bat
  README.md
  Proyecto1/
    Datos/datos.xml
    Interfaz/MenuConsola.cs
    Modelos/Registro.cs
    Servicios/GestorXML.cs
    Servicios/Buscador.cs
    Servicios/Validador.cs
    Servicios/Encriptador.cs
    Program.cs
    Proyecto1.csproj
    Manual_Funcionalidad_Proyecto1.docx.pdf
```

## 3. Compilar y ejecutar

### Opcion A: usando el archivo .bat (recomendada)
1. Abrir la carpeta del proyecto en el Explorador de archivos.
2. Hacer doble clic en `ejecutar.bat`.

O desde CMD:

```
cd MIA_Proyecto_En_Grupo
ejecutar.bat
```

El .bat compila el proyecto y abre el programa en la consola. [REVISAR: confirmar que hace tu .bat]

### Opcion B: manualmente por consola
```
cd MIA_Proyecto_En_Grupo\Proyecto1
dotnet build
dotnet run
```

### Opcion C: Visual Studio
1. Abrir `Proyecto1.csproj` con Visual Studio.
2. Presionar F5 o el boton Iniciar.

## 4. Uso del programa (paso a paso)

Al ejecutar se muestra el menu principal en consola. [REVISAR: poner el menu tal cual lo muestra tu programa]

```
1. Crear registro
2. Listar registros
3. Buscar por ID
4. Modificar registro
5. Eliminar registro
6. Encriptar archivo
7. Desencriptar archivo
0. Salir
```

### 4.1 Crear un registro
1. Escribir `1` y presionar Enter.
2. Ingresar los datos que pide el programa (ID, nombre del curso, etc.). [REVISAR: campos reales de Registro.cs]
3. El programa valida los datos y los guarda en `Datos/datos.xml`.

### 4.2 Listar registros
1. Escribir `2` y presionar Enter.
2. Se muestran todos los registros guardados.

### 4.3 Buscar por ID
1. Escribir `3` y presionar Enter.
2. Ingresar el ID a buscar.
3. Se muestra el registro, o un mensaje si no existe.

### 4.4 Modificar un registro
1. Escribir `4` y presionar Enter.
2. Ingresar el ID del registro a modificar.
3. Ingresar los nuevos datos.

### 4.5 Eliminar un registro
1. Escribir `5` y presionar Enter.
2. Ingresar el ID del registro a eliminar.
3. Confirmar la eliminacion.

### 4.6 Encriptar y desencriptar
1. Escribir `6` para encriptar el archivo XML.
2. Escribir `7` para desencriptarlo.
3. [REVISAR: indicar si pide clave y que algoritmo usa Encriptador.cs]

### 4.7 Salir
Escribir `0` y presionar Enter.

## 5. Solucion de problemas

| Problema | Solucion |
|---|---|
| `dotnet` no se reconoce | Instalar .NET y reiniciar la consola |
| El .bat se cierra solo | Ejecutarlo desde CMD para ver el error |
| No encuentra datos.xml | Verificar que exista la carpeta `Proyecto1\Datos` |
| Error al desencriptar | Usar la misma clave con la que se encripto |