# DeflateCompressor

Inicializa tareas de **compresión y descompresión de archivos** utilizando el algoritmo **Deflate**

## Métodos

▶ `CompressStream(Stream, Stream, CompressionLevel, long, Action<long, long>)`

Comprime un flujo de datos de entrada y escribe el resultado en el flujo de salida utilizando el algoritmo Deflate.

### Parámetros

* input: flujo que contiene los datos a comprimir.
* output: flujo de salida donde se escribirá el resultado comprimido.
* level: nivel de compresión (rápido, óptimo o sin compresión).
* maxBytes: cantidad máxima de bytes a procesar (-1 para ilimitado).
* progressCallback: callback opcional para reportar progreso.

---

▶ `CompressFile(string, string, CompressionLevel, Action<long, long>)`

Comprime el contenido de un archivo utilizando el algoritmo Deflate.

### Parámetros

* inputPath: ruta del archivo que se desea comprimir.
* outputPath: ruta donde se almacenará el archivo comprimido.
* level: nivel de compresión utilizado.
* progressCallback: callback opcional para reportar progreso.

### Ejemplo

```csharp
DeflateCompressor.CompressFile("entrada.txt", "salida.dfl", CompressionLevel.Optimal);
```

---

▶ `DecompressStream(Stream, Stream, long, Action<long, long>)`

Descomprime un flujo de datos comprimido con Deflate y escribe el resultado en el flujo de salida.

### Parámetros

* input: flujo comprimido a descomprimir.
* output: flujo donde se escribirá el resultado descomprimido.
* maxBytes: cantidad máxima de bytes a procesar (-1 para ilimitado).
* progressCallback: callback opcional para reportar progreso.

---

▶ `DecompressFile(string, string, Action<long, long>)`

Descomprime un archivo comprimido con el algoritmo Deflate.

### Parámetros

* inputPath: ruta del archivo comprimido.
* outputPath: ruta donde se almacenará el archivo descomprimido.
* progressCallback: callback opcional para reportar progreso.

### Ejemplo

```csharp
DeflateCompressor.DecompressFile("archivo.dfl", "archivo.txt");
```