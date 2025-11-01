# PlatformHelper

Proporciona propiedades y métodos auxiliares para identificar la plataforma del sistema operativo y ejecutar comandos específicos en Linux o macOS.

## Propiedades

▶ `IsWindows`

Indica si la plataforma actual es Windows.

▶ `IsLinux`

Indica si la plataforma actual es Linux.

▶ `IsMacOS`

Indica si la plataforma actual es macOS.

## Métodos

---

▶ `ExecuteXCommand(string)`

Ejecuta un comando en sistemas Linux o macOS usando `/bin/bash` y devuelve la salida.

### Parámetros

* command: comando a ejecutar en la terminal.

### Resultado

Cadena con la salida del comando, sin saltos de línea al final.

### Ejemplo

```csharp
string resultado = PlatformHelper.ExecuteXCommand("ls -la");
Console.WriteLine(resultado);
```