# XXLua

Proporciona funciones para **cifrar y descifrar scripts Lua** de *PvZ All Stars* utilizando el algoritmo **XXTEA**

## Métodos

▶ `EncryptStream(Stream, Stream)`

Cifra un flujo de datos usando el algoritmo XXTEA y escribe los datos resultantes en el flujo de salida.

### Parámetros

* input: flujo de entrada con los datos a cifrar.
* output: flujo de salida donde se escribirá el contenido cifrado.

### Ejemplo

```csharp
using FileStream input = File.OpenRead("script.lua");
using FileStream output = File.Create("script.lua.enc");
XXLua.EncryptStream(input, output);
```

---

▶ `EncryptFile(string, string)`

Cifra un archivo del disco aplicando XXTEA y genera una versión cifrada con encabezado de identificación.

### Parámetros

* inputPath: ruta del archivo Lua a cifrar.
* outputPath: ubicación donde se guardará el archivo cifrado.

### Ejemplo

```csharp
XXLua.EncryptFile("data/main.lua", "data/main_enc.lua");
```

---

▶ `DecryptStream(Stream, Stream)`

Descifra un flujo previamente cifrado con XXTEA y restaura los datos originales al flujo de salida.

### Parámetros

* input: flujo de entrada con los datos cifrados.
* output: flujo de salida donde se guardarán los datos descifrados.

### Ejemplo

```csharp
using FileStream input = File.OpenRead("script.lua.enc");
using FileStream output = File.Create("script_dec.lua");
XXLua.DecryptStream(input, output);
```

---

▶ `DecryptFile(string, string)`

Descifra un archivo Lua cifrado mediante XXTEA, eliminando el encabezado y devolviendo el script original.

### Parámetros

* inputPath: ruta del archivo cifrado.
* outputPath: ruta donde se guardará el archivo descifrado.

### Ejemplo

```csharp
XXLua.DecryptFile("data/main_enc.lua", "data/main.lua");
```