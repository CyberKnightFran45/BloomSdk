# XorCryptor

Proporciona funciones para cifrado **XOR** de **arrays de bytes, streams y archivos**

### Notas

- *Se puede usar la misma función para cifrar o descifrar, aplicando la misma clave.*

- *Es un cifrado muy simple y **no seguro para datos críticos**. Solo útil para ofuscación ligera o pruebas.*

## Métodos

▶ `CipherData(ReadOnlySpan<byte>, ReadOnlySpan<byte>)`

Cifra o descifra un arreglo de bytes usando XOR.

### Parámetros

* input: datos a cifrar/descifrar.
* key: clave de cifrado.

### Resultado

* Retorna `NativeMemoryOwner<byte>` con los datos cifrados/descifrados.

### Ejemplo

```csharp
byte[] data = System.Text.Encoding.UTF8.GetBytes("Hola Mundo");
byte[] key = System.Text.Encoding.UTF8.GetBytes("clave");
var ciphered = XorCryptor.CipherData(data, key);
```

---

▶ `CipherStream(Stream, Stream, ReadOnlySpan<byte>, long, Action<long, long>)`

Cifra o descifra un stream completo.

### Parámetros

* input: stream de origen.
* output: stream de salida.
* key: clave de cifrado.
* maxBytes: opcional, máximo de bytes a procesar.
* progressCallback: opcional, callback de progreso.

---

▶ `CipherFile(string, string, ReadOnlySpan<byte>, Action<long, long>)`

Cifra o descifra un archivo completo usando XOR.

### Parámetros

* inputPath: ruta del archivo original.
* outputPath: ruta donde se guardará el archivo cifrado.
* key: clave de cifrado.
* progressCallback: opcional, callback de progreso.