# XXTeaCryptor

Implementa el cifrado **Corrected Block TEA (XXTEA)**

Autores: David Wheeler, Roger Needham y Ma Xiaoyun

## Notas

- _Clave fija de **16 bytes**_
- _Los datos se convierten internamente a bloques de 4 bytes (uint)._
- _Adecuado para cifrado de bloques de datos en memoria; no es lo más rápido para streams gigantes sin segmentar._

---

## Métodos

▶ `EncryptData(ReadOnlySpan<byte>, ReadOnlySpan<byte>)`

Cifra un arreglo de bytes con XXTEA.

### Parámetros

* input: datos a cifrar.
* key: clave de 16 bytes.

### Resultado

* Retorna `NativeMemoryOwner<byte>` con los datos cifrados.

### Ejemplo

```csharp
byte[] data = System.Text.Encoding.UTF8.GetBytes("Hola Mundo");
byte[] key = new byte[16]; // clave de 16 bytes
var encrypted = XXTeaCryptor.EncryptData(data, key);
```

---

▶ `DecryptData(ReadOnlySpan<byte>, ReadOnlySpan<byte>)`

Descifra un arreglo de bytes cifrado con `EncryptData`.

### Parámetros

* input: datos cifrados.
* key: misma clave de 16 bytes usada para cifrar.

### Resultado

* Retorna `NativeMemoryOwner<byte>` con los datos descifrados.

### Ejemplo

```csharp
var decrypted = XXTeaCryptor.DecryptData(encrypted.AsSpan(), key);
string original = System.Text.Encoding.UTF8.GetString(decrypted.AsSpan());
```