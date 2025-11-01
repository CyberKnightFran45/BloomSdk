# RijndaelEngine

Implementación optimizada de **Rijndael** para C#. Permite cifrar y descifrar bloques de bytes con tamaños de bloque y claves flexibles.

## Constructores

▶ `RijndaelEngine(int, ReadOnlySpan<byte>)`

Inicializa un motor Rijndael configurando el tamaño de bloque, la longitud de clave y genera la clave expandida (roundKey) para todas las rondas.

### Parámetros

* blockSizeBits: tamaño del bloque en bits (128, 160, 192, 224 ó 256)
* key: clave de cifrado como arreglo de bytes.

### Ejemplo

```csharp
byte[] key = new byte[16]; // 128-bit key
RijndaelEngine engine = new(128, key);
```

## Métodos

▶ `EncryptBlock(ReadOnlySpan<byte>, Span<byte>)`

Cifra un bloque completo de datos usando las rondas de Rijndael.

### Parámetros

* input: bloque de bytes a cifrar (debe coincidir con el tamaño de bloque).
* output: bloque de bytes donde se escribirá el resultado cifrado.

### Resultado

Bloque cifrado escrito en `output`.

### Ejemplo

```csharp
byte[] inputBlock = new byte[16];
byte[] outputBlock = new byte[16];
engine.EncryptBlock(inputBlock, outputBlock);
```

---

▶ `DecryptBlock(ReadOnlySpan<byte>, Span<byte>)`

Descifra un bloque completo aplicando las rondas inversas de Rijndael.

### Parámetros

* input: bloque de bytes cifrado (debe coincidir con el tamaño de bloque).
* output: bloque de bytes donde se escribirá el resultado descifrado.

### Resultado

Bloque descifrado escrito en `output`.

### Ejemplo

```csharp
byte[] cipherBlock = new byte[16];
byte[] plainBlock = new byte[16];
engine.DecryptBlock(cipherBlock, plainBlock);
```