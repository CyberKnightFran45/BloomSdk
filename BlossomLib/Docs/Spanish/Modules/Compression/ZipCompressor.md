# ZipCompressor

Inicializa la compresión y descompresión de archivos o directorios utilizando el algoritmo **ZIP**

## Métodos

▶ `CompressStream(string, Stream, CompressionLevel, Action<string,long,long>)`

Comprime un archivo o directorio y escribe el resultado en un flujo de salida ZIP.

### Parámetros

* input: ruta del archivo o carpeta que se desea comprimir.
* output: flujo de salida donde se escribirá el archivo ZIP.
* level: nivel de compresión a aplicar.
* progressCallback: callback opcional que informa el nombre del archivo, bytes escritos y bytes totales.

### Ejemplo

```csharp
using var output = File.Create("backup.zip");
ZipCompressor.CompressStream("C:\\Project", output, CompressionLevel.Optimal);
```

---

▶ `Compress(string, string, CompressionLevel, Action<string,long,long>)`

Comprime un archivo o carpeta en un archivo ZIP físico.

### Parámetros

* inputPath: ruta del archivo o carpeta que se desea comprimir.
* outputFile: ruta de salida del archivo ZIP.
* level: nivel de compresión a aplicar.
* progressCallback: callback opcional que informa el nombre del archivo, bytes escritos y bytes totales.

### Ejemplo

```csharp
ZipCompressor.Compress("C:\\Data", "data_archive.zip", CompressionLevel.Fastest);
```

---

▶ `DecompressStream(Stream, string, Action<string,long,long>)`

Descomprime el contenido de un flujo ZIP en un directorio de destino.

### Parámetros

* input: flujo de entrada con el contenido comprimido.
* outputDir: carpeta donde se extraerán los archivos.
* progressCallback: callback opcional que informa el nombre del archivo, bytes escritos y bytes totales.

### Ejemplo

```csharp
using var input = File.OpenRead("backup.zip");
ZipCompressor.DecompressStream(input, "RestoredData");
```

---

▶ `Decompress(string, string, Action<string,long,long>)`

Descomprime un archivo ZIP completo en un directorio.

### Parámetros

* inputPath: ruta del archivo ZIP que se desea descomprimir.
* outputDir: carpeta donde se extraerán los archivos.
* progressCallback: callback opcional que informa el nombre del archivo, bytes escritos y bytes totales.

### Ejemplo

```csharp
ZipCompressor.Decompress("data_archive.zip", "C:\\Restored");
```