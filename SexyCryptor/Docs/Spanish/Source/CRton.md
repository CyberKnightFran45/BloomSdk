# CRton

Proporciona funciones para **cifrar y descifrar archivos RTON** utilizados en la versión china de PvZ 2, mediante el cifrado Rijndael.

## Notas

* El encabezado de los archivos cifrados se identifica con `10 00`
* Usa un bloque Rijndael de tamaño 24 para el cifrado.
* La clave y el vector de inicialización se generan internamente a partir de un valor fijo y MD5.

## Métodos

▶ `EncryptStream(Stream, Stream)`

Cifra un flujo de datos escribiendo la salida directamente en otro flujo.

### Parámetros

* input: flujo de entrada con los datos a cifrar.
* output: flujo de salida donde se guardarán los datos cifrados.

### Ejemplo

```csharp
using FileStream inFile = File.OpenRead("input.rton");
using FileStream outFile = File.Create("output.rton");
CRton.EncryptStream(inFile, outFile);
```

---

▶ `EncryptFile(string, string)`

Cifra un archivo RTON utilizando el cifrado Rijndael y lo guarda en la ubicación especificada.

### Parámetros

* inputPath: ruta del archivo a cifrar.
* outputPath: ruta donde se guardará el archivo cifrado.

### Ejemplo

```csharp
CRton.EncryptFile("input.rton", "encrypted.rton");
```

---

▶ `DecryptStream(Stream, Stream)`

Descifra un flujo de datos previamente cifrado, escribiendo la salida en otro flujo.

### Parámetros

* input: flujo de entrada con datos cifrados.
* output: flujo de salida donde se guardarán los datos descifrados.

### Ejemplo

```csharp
using FileStream inFile = File.OpenRead("encrypted.rton");
using FileStream outFile = File.Create("decrypted.rton");
CRton.DecryptStream(inFile, outFile);
```

---

▶ `DecryptFile(string, string)`

Descifra un archivo RTON cifrado y guarda el resultado en la ruta especificada.

### Parámetros

* inputPath: ruta del archivo cifrado.
* outputPath: ruta donde se guardará el archivo descifrado.

### Ejemplo

```csharp
CRton.DecryptFile("encrypted.rton", "decrypted.rton");
```