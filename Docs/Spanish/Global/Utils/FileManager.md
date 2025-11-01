# FileManager

Esta clase proporciona funciones auxiliares para trabajar con archivos, incluyendo apertura de streams, cálculo de tamaños y procesamiento de datos en memoria.

## Métodos

▶ `GetFileSize(string)`

Obtiene el tamaño de un archivo en bytes.

### Parámetros

* `path`: ruta del archivo a analizar.

### Resultado

Un `long` que representa el tamaño del archivo en bytes.

### Ejemplo

```csharp
long size = FileManager.GetFileSize("document.txt");
```

---

▶ `FileIsEmpty(string)`

Verifica si un archivo está vacío.

### Parámetros

* `path`: ruta del archivo a verificar.

### Resultado

`true` si el archivo está vacío; `false` en caso contrario.

### Ejemplo

```csharp
bool empty = FileManager.FileIsEmpty("document.txt");
```

---

▶ `Open(string)`

Abre un archivo para lectura y escritura.

### Parámetros

* `filePath`: ruta del archivo a abrir.

### Resultado

Un `FileStream` con acceso completo al archivo.

### Ejemplo

```csharp
using FileStream fs = FileManager.Open("document.txt");
```

---

▶ `OpenRead(string)`

Abre un archivo solo para lectura.

### Parámetros

* `filePath`: ruta del archivo a abrir.

### Resultado

Un `FileStream` de solo lectura.

### Ejemplo

```csharp
using FileStream fs = FileManager.OpenRead("document.txt");
```

---

▶ `OpenWrite(string)`

Abre un archivo solo para escritura, creándolo si no existe.

### Parámetros

* `filePath`: ruta del archivo a abrir.

### Resultado

Un `FileStream` de escritura.

### Ejemplo

```csharp
using FileStream fs = FileManager.OpenWrite("document.txt");
```

---

▶ `Process(Stream, Stream, long, Action<long, long>)`

Procesa los datos de un stream y los escribe en otro stream.

### Parámetros

* `input`: stream de entrada.
* `output`: stream de salida.
* `maxBytes`: número máximo de bytes a procesar. Valor por defecto: `-1`.
* `progressCallback`: callback opcional para reportar progreso. Valor por defecto: `null`.

### Ejemplo

```csharp
FileManager.Process(inputStream, outputStream, maxBytes: 1024, progressCallback: (read, total) => Console.WriteLine(read));
```

---

▶ `Process(Stream, Stream, BytesTransform, long, Action<long, long>)`

Procesa los datos de un stream aplicando una función de transformación y los escribe en otro stream.

### Parámetros

* `input`: stream de entrada.
* `output`: stream de salida.
* `transform`: función que transforma el buffer.
* `maxBytes`: número máximo de bytes a procesar. Valor por defecto: `-1`.
* `progressCallback`: callback opcional para reportar progreso. Valor por defecto: `null`.

### Ejemplo

```csharp
FileManager.Process(inputStream, outputStream, chunk => chunk.ToArray());
```

---

▶ `Process(Stream, Stream, ReadOnlySpan<byte>, BytesTransform2, long, Action<long, long>)`

Procesa los datos de un stream aplicando una función de transformación con un argumento adicional y los escribe en otro stream.

### Parámetros

* `input`: stream de entrada.
* `output`: stream de salida.
* `arg`: argumento extra pasado a la función de transformación.
* `transform`: función que transforma el buffer usando `arg`.
* `maxBytes`: número máximo de bytes a procesar. Valor por defecto: `-1`.
* `progressCallback`: callback opcional para reportar progreso. Valor por defecto: `null`.

### Ejemplo

```csharp
FileManager.Process(inputStream, outputStream, argBytes, (chunk, arg) => chunk.ToArray());
```

## Delegados

| Delegado          |                                                            Descripción                                                           |
| ----------------- | :------------------------------------------------------------------------------------------------------------------------------: |
| `BytesTransform`  |               Función que transforma un bloque de bytes (`ReadOnlySpan<byte>`) y retorna `NativeMemoryOwner<byte>`.              |
| `BytesTransform2` | Función que transforma un bloque de bytes con un argumento adicional (`ReadOnlySpan<byte>`) y retorna `NativeMemoryOwner<byte>`. |
