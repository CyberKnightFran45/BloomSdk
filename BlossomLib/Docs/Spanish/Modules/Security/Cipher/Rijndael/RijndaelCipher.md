# RijndaelCipher

Permite cifrar y descifrar datos utilizando el algoritmo Rijndael mediante punteros de memoria nativa. Ofrece soporte para múltiples modos de operación y esquemas de padding personalizados.

## Constructores

▶ `ctor(RijndaelBlockSize, ReadOnlySpan<byte>, RijndaelMode, RijndaelPadding, ReadOnlySpan<byte>)`

Inicializa una nueva instancia del cifrador Rijndael con los parámetros especificados.

### Parámetros

* blockSize: Tamaño del bloque del algoritmo, expresado en bits.
* key: Clave utilizada para el cifrado y descifrado.
* mode: Modo de cifrado a emplear (por defecto CBC).
* padding: Esquema de relleno a aplicar (por defecto ZeroPadding).
* iv: Vector de inicialización opcional.

### Ejemplo

```csharp
var cipher = new RijndaelCipher(
    RijndaelBlockSize.SIZE_32,
    key: myKey,
    mode: RijndaelMode.CBC,
    padding: RijndaelPadding.Pkcs7,
    iv: myIV);
```

---

## Métodos

▶ `Cipher(ReadOnlySpan<byte>, bool)`

Cifra o descifra los datos proporcionados según el modo especificado.

### Parámetros

* data: Datos a procesar.
* forEncryption: Si es `true`, cifra; si es `false`, descifra.

### Resultado

Los datos cifrados o descifrados, según corresponda.

### Ejemplo

```csharp
using var cipher = new RijndaelCipher(RijndaelBlockSize.SIZE_32, key);
using var encrypted = cipher.Cipher(data, true);
using var decrypted = cipher.Cipher(encrypted.AsSpan(), false);
```

---

▶ `Dispose()`

Libera los recursos de memoria nativa utilizados por la instancia del cifrador.

### Ejemplo

```csharp
using var cipher = new RijndaelCipher(RijndaelBlockSize.SIZE_16, key);
```