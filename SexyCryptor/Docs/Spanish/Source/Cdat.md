# Cdat

Proporciona funciones para **cifrar y descifrar archivos C-dat** utilizados en PvZ Free (Android), principalmente imágenes PNG.

## Notas

* Los archivos cifrados incluyen un encabezado `"CRYPT_RES"` y un identificador de tipo `0x0A`.
* Solo los primeros 256 bytes se cifran con XOR usando una clave interna, el resto se copia directamente.

## Métodos

▶ `EncryptStream(Stream, Stream, Action<long,long>)`

Cifra un flujo de datos aplicando XOR sobre los primeros bytes y escribiendo el resultado en otro flujo.

### Parámetros

* input: flujo de entrada con los datos a cifrar.
* output: flujo de salida donde se guardarán los datos cifrados.
* progressCallback: callback opcional que recibe el progreso en bytes procesados.

### Ejemplo

```csharp
using FileStream inFile = File.OpenRead("image.png");
using FileStream outFile = File.Create("image.cdat");
Cdat.EncryptStream(inFile, outFile);
```

---

▶ `EncryptFile(string, string, Action<long,long>)`

Cifra un archivo C-dat aplicando XOR y guardando el resultado en la ruta especificada.

### Parámetros

* inputPath: ruta del archivo a cifrar.
* outputPath: ruta donde se guardará el archivo cifrado.
* progressCallback: callback opcional que recibe el progreso en bytes procesados.

### Ejemplo

```csharp
Cdat.EncryptFile("image.png", "image.cdat");
```

---

▶ `DecryptStream(Stream, Stream, Action<long,long>)`

Descifra un flujo de datos previamente cifrado y escribe el resultado en otro flujo.

### Parámetros

* input: flujo de entrada con datos cifrados.
* output: flujo de salida donde se guardarán los datos descifrados.
* progressCallback: callback opcional que recibe el progreso en bytes procesados.

### Ejemplo

```csharp
using FileStream inFile = File.OpenRead("image.cdat");
using FileStream outFile = File.Create("image.png");
Cdat.DecryptStream(inFile, outFile);
```

---

▶ `DecryptFile(string, string, Action<long,long>)`

Descifra un archivo C-dat cifrado con XOR y guarda el resultado en la ruta especificada.

### Parámetros

* inputPath: ruta del archivo cifrado.
* outputPath: ruta donde se guardará el archivo descifrado.
* progressCallback: callback opcional que recibe el progreso en bytes procesados.

### Ejemplo

```csharp
Cdat.DecryptFile("image.cdat", "image.png");
```