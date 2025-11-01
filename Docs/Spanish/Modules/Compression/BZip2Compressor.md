# BZip2Compressor

Inicializa tareas de **compresión y descompresión de archivos** utilizando el algoritmo **BZip2**, con soporte para flujo directo de datos.

## Métodos

▶ `CompressStream(Stream, Stream, int, long, Action<long, long>)`

Comprime un flujo de entrada y escribe el resultado comprimido en un flujo de salida usando el algoritmo BZip2.

### Parámetros

* input: flujo de datos a comprimir.
* output: flujo de salida donde se escribirá el contenido comprimido.
* blockSize: tamaño del bloque de compresión.
* maxBytes: máximo de bytes a procesar (-1 para ilimitado).
* progressCallback: callback opcional para reportar progreso.

---

▶ `CompressFile(string, string, int, Action<long, long>)`

Comprime el contenido de un archivo mediante el algoritmo BZip2.

### Parámetros

* inputPath: ruta del archivo a comprimir.
* outputPath: ruta donde se guardará el archivo comprimido.
* blockSize: tamaño del bloque de compresión.
* progressCallback: callback opcional para reportar progreso.

### Ejemplo

```csharp
BZip2Compressor.CompressFile("entrada.txt", "salida.bz2", 9);
```

---

▶ `DecompressStream(Stream, Stream, long, Action<long, long>)`

Descomprime un flujo de datos comprimido en formato BZip2 y escribe el resultado plano en un flujo de salida.

### Parámetros

* input: flujo comprimido a descomprimir.
* output: flujo de salida donde se escribirá el contenido descomprimido.
* maxBytes: máximo de bytes a procesar (-1 para ilimitado).
* progressCallback: callback opcional para reportar progreso.

---

▶ `DecompressFile(string, string, Action<long, long>)`
Descomprime un archivo previamente comprimido en formato BZip2.

### Parámetros

* inputPath: ruta del archivo comprimido.
* outputPath: ruta donde se guardará el archivo descomprimido.
* progressCallback: callback opcional para reportar progreso.

### Ejemplo

```csharp
BZip2Compressor.DecompressFile("archivo.bz2", "archivo.txt");
```