# ZLibCompressor

Inicializa la compresión y descompresión de archivos utilizando el algoritmo **ZLib**

## Métodos

▶ `CompressStream(Stream, Stream, CompressionLevel, long, Action<long,long>)`

Comprime los datos de un flujo de entrada y los escribe en un flujo de salida utilizando el algoritmo ZLib.

### Parámetros

* input: flujo de entrada con los datos originales.
* output: flujo de salida donde se escriben los datos comprimidos.
* level: nivel de compresión a aplicar.
* maxBytes: cantidad máxima de bytes a procesar (-1 para ilimitado).
* progressCallback: callback opcional para seguimiento del progreso.

### Ejemplo

```csharp
using var input = File.OpenRead("data.txt");
using var output = File.Create("data.zlib");
ZLibCompressor.CompressStream(input, output, CompressionLevel.Optimal);
```

---

▶ `CompressFile(string, string, CompressionLevel, Action<long,long>)`

Comprime el contenido de un archivo utilizando el algoritmo ZLib.

### Parámetros

* inputPath: ruta del archivo que se desea comprimir.
* outputPath: ruta donde se guardará el archivo comprimido.
* level: nivel de compresión a aplicar.
* progressCallback: callback opcional para seguimiento del progreso.

### Ejemplo

```csharp
ZLibCompressor.CompressFile("input.bin", "compressed.zlib", CompressionLevel.Fastest);
```

---

▶ `DecompressStream(Stream, Stream, long, Action<long,long>)`

Descomprime un flujo comprimido en formato ZLib y lo escribe en un flujo de salida.

### Parámetros

* input: flujo comprimido de entrada.
* output: flujo donde se escribirá el resultado descomprimido.
* maxBytes: cantidad máxima de bytes a procesar (-1 para ilimitado).
* progressCallback: callback opcional para seguimiento del progreso.

### Ejemplo

```csharp
using var input = File.OpenRead("archive.zlib");
using var output = File.Create("archive.txt");
ZLibCompressor.DecompressStream(input, output);
```

---

▶ `DecompressFile(string, string, Action<long,long>)`

Descomprime el contenido de un archivo en formato ZLib.

### Parámetros

* inputPath: ruta del archivo comprimido que se desea descomprimir.
* outputPath: ruta donde se guardará el archivo descomprimido.
* progressCallback: callback opcional para seguimiento del progreso.

### Ejemplo

```csharp
ZLibCompressor.DecompressFile("backup.zlib", "backup_restored.txt");
```