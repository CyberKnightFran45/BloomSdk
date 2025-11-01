# GZipCompressor

Inicializa tareas de compresión y descompresión de archivos utilizando el algoritmo **GZip**

## Métodos

▶ `CompressStream(Stream, Stream, CompressionLevel, long, Action<long,long>)`

Comprime los datos de un flujo de entrada y los escribe en un flujo de salida usando GZip.

### Parámetros

* input: flujo que contiene los datos de entrada.
* output: flujo donde se escriben los datos comprimidos.
* level: nivel de compresión a aplicar.
* maxBytes: cantidad máxima de bytes a procesar (-1 para ilimitado).
* progressCallback: callback opcional para reportar progreso.

### Ejemplo

```csharp
using var input = File.OpenRead("data.txt");
using var output = File.Create("data.gz");
GZipCompressor.CompressStream(input, output, CompressionLevel.Optimal);
```

---

▶ `CompressFile(string, string, CompressionLevel, Action<long,long>)`

Comprime el contenido de un archivo mediante el algoritmo GZip.

### Parámetros

* inputPath: ruta del archivo que se desea comprimir.
* outputPath: ruta donde se guardará el archivo comprimido.
* level: nivel de compresión a aplicar.
* progressCallback: callback opcional para reportar progreso.

### Ejemplo

```csharp
GZipCompressor.CompressFile("report.log", "report.gz", CompressionLevel.Fastest);
```

---

▶ `DecompressStream(Stream, Stream, long, Action<long,long>)`

Descomprime los datos de un flujo GZip y los escribe en un flujo de salida.

### Parámetros

* input: flujo que contiene los datos comprimidos.
* output: flujo donde se escribirán los datos descomprimidos.
* maxBytes: cantidad máxima de bytes a procesar (-1 para ilimitado).
* progressCallback: callback opcional para reportar progreso.

### Ejemplo

```csharp
using var input = File.OpenRead("archive.gz");
using var output = File.Create("archive.txt");
GZipCompressor.DecompressStream(input, output);
```

---

▶ `DecompressFile(string, string, Action<long,long>)`

Descomprime un archivo comprimido en formato GZip.

### Parámetros

* inputPath: ruta del archivo comprimido a descomprimir.
* outputPath: ruta donde se guardará el archivo resultante.
* progressCallback: callback opcional para reportar progreso.

### Ejemplo

```csharp
GZipCompressor.DecompressFile("backup.gz", "backup_restored.txt");
```