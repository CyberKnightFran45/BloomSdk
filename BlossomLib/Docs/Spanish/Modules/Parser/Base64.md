# Base64

Proporciona utilidades para codificar y decodificar datos en **Base64**, soportando variantes estándar y web-safe, incluyendo operaciones sobre `Stream` y `File`.

## Métodos

▶ `GetEncodedLength(long)`

Obtiene la longitud necesaria para codificar una cantidad de bytes en Base64 estándar.

### Parámetros

* `len`: cantidad de bytes a codificar.

### Resultado

Longitud requerida para la cadena Base64.

### Ejemplo

```csharp
long length = BlossomLib.Modules.Parsers.Base64.GetEncodedLength(10);
```

---

▶ `GetEncodedLengthUtf8(int)`

Obtiene la longitud máxima requerida para codificar bytes en Base64 UTF-8.

### Parámetros

* `len`: cantidad de bytes a codificar.

### Resultado

Longitud máxima en bytes del Base64 UTF-8.

### Ejemplo

```csharp
int length = BlossomLib.Modules.Parsers.Base64.GetEncodedLengthUtf8(10);
```

---

▶ `EncodeBytes(ReadOnlySpan<byte>, bool)`

Codifica un arreglo de bytes en Base64, opcionalmente en versión web-safe.

### Parámetros

* `input`: bytes a codificar.
* `isWebSafe`: determina si se genera Base64 web-safe.

### Resultado

`NativeString` con la cadena Base64 generada.

### Ejemplo

```csharp
byte[] data = {1,2,3};
var base64 = BlossomLib.Modules.Parsers.Base64.EncodeBytes(data, true);
```

---

▶ `EncodeBytesUtf8(ReadOnlySpan<byte>, bool)`

Codifica bytes directamente en Base64 UTF-8, opcionalmente web-safe.

### Parámetros

* `input`: bytes a codificar.
* `isWebSafe`: determina si se genera Base64 web-safe.

### Resultado

`NativeMemoryOwner<byte>` con los bytes codificados.

### Ejemplo

```csharp
byte[] data = {1,2,3};
var base64Bytes = BlossomLib.Modules.Parsers.Base64.EncodeBytesUtf8(data, true);
```

---

▶ `EncodeStream(Stream, Stream, bool, long, Action<long,long>)`

Codifica un flujo completo a Base64 y escribe el resultado en otro flujo.

### Parámetros

* `input`: flujo de entrada.
* `output`: flujo de salida.
* `isWebSafe`: determina si se genera Base64 web-safe.
* `maxBytes`: máximo de bytes a procesar (-1 para ilimitado).
* `progressCallback`: callback opcional para reportar progreso.

### Ejemplo

```csharp
using var input = File.OpenRead("input.bin");
using var output = File.OpenWrite("output.b64");
BlossomLib.Modules.Parsers.Base64.EncodeStream(input, output, true);
```

---

▶ `EncodeFile(string, string, bool, Action<long,long>)`

Codifica un archivo a Base64 y guarda el resultado en otro archivo.

### Parámetros

* `inputPath`: ruta del archivo a codificar.
* `outputPath`: ruta donde guardar el archivo codificado.
* `isWebSafe`: determina si se genera Base64 web-safe.
* `progressCallback`: callback opcional para reportar progreso.

### Ejemplo

```csharp
BlossomLib.Modules.Parsers.Base64.EncodeFile("input.bin", "output.b64", true);
```

---

▶ `DecodeString(ReadOnlySpan<char>, bool)`

Decodifica una cadena Base64 a bytes, soportando variante web-safe.

### Parámetros

* `input`: cadena Base64 a decodificar.
* `isWebSafe`: indica si la cadena es web-safe.

### Resultado

`NativeMemoryOwner<byte>` con los bytes decodificados.

### Ejemplo

```csharp
var bytes = BlossomLib.Modules.Parsers.Base64.DecodeString("SGVsbG8=", false);
```

---

▶ `DecodeUtf8Bytes(ReadOnlySpan<byte>, bool)`

Decodifica bytes Base64 en UTF-8, soportando web-safe.

### Parámetros

* `input`: bytes codificados en Base64 UTF-8.
* `isWebSafe`: indica si la codificación es web-safe.

### Resultado

`NativeMemoryOwner<byte>` con los bytes decodificados.

### Ejemplo

```csharp
var bytes = BlossomLib.Modules.Parsers.Base64.DecodeUtf8Bytes(Encoding.UTF8.GetBytes("SGVsbG8="), false);
```

---

▶ `DecodeStream(Stream, Stream, bool, long, Action<long,long>)`

Decodifica un flujo Base64 completo y escribe el resultado en otro flujo.

### Parámetros

* `input`: flujo Base64 de entrada.
* `output`: flujo de salida.
* `isWebSafe`: determina si se decodifica Base64 web-safe.
* `maxBytes`: máximo de bytes a procesar (-1 para ilimitado).
* `progressCallback`: callback opcional para reportar progreso.

### Ejemplo

```csharp
using var input = File.OpenRead("input.b64");
using var output = File.OpenWrite("decoded.bin");
BlossomLib.Modules.Parsers.Base64.DecodeStream(input, output, true);
```

---

▶ `DecodeFile(string, string, bool, Action<long,long>)`

Decodifica un archivo Base64 y guarda los bytes decodificados en otro archivo.

### Parámetros

* `inputPath`: ruta del archivo Base64 a decodificar.
* `outputPath`: ruta donde se guardarán los bytes decodificados.
* `isWebSafe`: indica si el archivo Base64 es web-safe.
* `progressCallback`: callback opcional para reportar progreso.

### Ejemplo

```csharp
BlossomLib.Modules.Parsers.Base64.DecodeFile("input.b64", "output.bin", true);
```